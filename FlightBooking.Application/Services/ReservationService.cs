using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooking.Application.Common;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Strategies.Pricing;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using FlightBooking.Domain.ValueObjects;
using IFlightRepository = FlightBooking.Application.Interfaces.Repositories.IFlightRepository;

namespace FlightBooking.Application.Services
{
    /// <summary>
    /// Service për logjikën e biznesit të rezervimeve
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightService _flightService;
        private readonly IPricingStrategy _pricingStrategy;

        public ReservationService(
            IReservationRepository reservationRepository,
            IFlightRepository flightRepository,
            IPassengerRepository passengerRepository,
            IFlightService flightService,
            IPricingStrategy pricingStrategy)
        {
            _reservationRepository = reservationRepository;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
            _flightService = flightService;
            _pricingStrategy = pricingStrategy;
        }

        /// <summary>
        /// Krijon një rezervim të ri
        /// </summary>
        public async Task<Reservation> CreateReservationAsync(
            int flightId,
            int passengerId,
            SeatClass seatClass)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[ReservationService] Duke filluar krijimin e rezervimit...");
            Console.WriteLine("============================================");

            // Validimet
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                throw new InvalidOperationException($"Fluturimi me ID {flightId} nuk u gjet");

            var passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new InvalidOperationException($"Pasagjeri me ID {passengerId} nuk u gjet");

            if (!flight.CanBeBooked())
                throw new InvalidOperationException("Ky fluturim nuk mund të rezervohet");

            if (flight.AvailableSeats <= 0)
                throw new InvalidOperationException("Nuk ka vende të disponueshme në këtë fluturim");

            // Llogarit çmimin duke përdorur STRATEGY PATTERN
            Console.WriteLine($"[ReservationService] Duke llogaritur çmimin me strategjinë: {_pricingStrategy.StrategyName}");
            var totalPriceMoney = _pricingStrategy.CalculatePrice(flight, seatClass, 1);
            var totalPrice = totalPriceMoney.Amount;
            Console.WriteLine($"[ReservationService] Çmimi bazë: €{flight.BasePriceAmount:F2}");
            Console.WriteLine($"[ReservationService] Klasa: {seatClass}");
            Console.WriteLine($"[ReservationService] Çmimi total: €{totalPrice:F2}");

            // Gjenero kod rezervimi unik
            var reservationCode = GenerateReservationCode();

            // Gjenero numër ulëseje
            var seatNumber = GenerateSeatNumber(seatClass);

            // Krijo rezervimin
            var reservation = new Reservation
            {
                ReservationCode = reservationCode,
                FlightId = flightId,
                PassengerId = passengerId,
                SeatClass = seatClass,
                SeatNumber = seatNumber,
                TotalPrice = totalPrice,
                Status = ReservationStatus.Pending,
                ReservationDate = DateTime.Now,
                Flight = flight,
                Passenger = passenger
            };

            // Ruaj rezervimin
            await _reservationRepository.AddAsync(reservation);
            Console.WriteLine($"[ReservationService] Rezervimi u krijua me kod: {reservationCode}");

            // Rezervo ulësen në fluturim
            await _flightService.ReserveSeatsAsync(flightId, 1);

            Console.WriteLine("============================================\n");

            return reservation;
        }


        /// <summary>
        /// Wrapper që ekzekuton CreateReservationAsync dhe kthen OperationResult për të shmangur exceptions
        /// Kjo ruan metoden origjinale për backward compatibility
        /// </summary>
        public async Task<OperationResult<Reservation>> TryCreateReservationAsync(
            int flightId,
            int passengerId,
            SeatClass seatClass)
        {
            try
            {
                var reservation = await CreateReservationAsync(flightId, passengerId, seatClass);
                return OperationResult<Reservation>.Success(reservation);
            }
            catch (Exception ex)
            {
                // këtu mund të logosh (shëno si TODO për të zëvendësuar Console.WriteLine me ILogger në hapin tjetër)
                return OperationResult<Reservation>.Failure(ex.Message, ex);
            }
        }

        /// <summary>
        /// Wrapper për ConfirmReservationAsync që kthen OperationResult (pa hequr exception-throwing variantin)
        /// </summary>
        public async Task<OperationResult> TryConfirmReservationAsync(int reservationId)
        {
            try
            {
                await ConfirmReservationAsync(reservationId);
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure(ex.Message, ex);
            }
        }

        /// <summary>
        /// Wrapper për CancelReservationAsync që kthen OperationResult (pa hequr exception-throwing variantin)
        /// </summary>
        public async Task<OperationResult> TryCancelReservationAsync(int reservationId)
        {
            try
            {
                await CancelReservationAsync(reservationId);
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure(ex.Message, ex);
            }
        }
        /// <summary>
        /// Merr rezervimin sipas kodit
        /// </summary>
        public async Task<Reservation?> GetReservationByCodeAsync(string reservationCode)
        {
            if (string.IsNullOrWhiteSpace(reservationCode))
                throw new ArgumentException("Kodi i rezervimit është i detyrueshëm", nameof(reservationCode));

            return await _reservationRepository.GetByReservationCodeAsync(reservationCode);
        }

        /// <summary>
        /// Konfirmon një rezervim (pasi pagesa është bërë)
        /// </summary>
        public async Task ConfirmReservationAsync(int reservationId)
        {
            Console.WriteLine($"\n[ReservationService] Duke konfirmuar rezervimin ID: {reservationId}");

            var reservation = await _reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
                throw new InvalidOperationException($"Rezervimi me ID {reservationId} nuk u gjet");

            if (reservation.Status != ReservationStatus.Pending)
                throw new InvalidOperationException($"Rezervimi me status {reservation.Status} nuk mund të konfirmohet");

            // Ndrysho statusin në Confirmed
            reservation.Status = ReservationStatus.Confirmed;

            await _reservationRepository.UpdateAsync(reservation);

            Console.WriteLine($"[ReservationService] Rezervimi {reservation.ReservationCode} u konfirmua me sukses!");
        }

        /// <summary>
        /// Anulon një rezervim
        /// </summary>
        public async Task CancelReservationAsync(int reservationId)
        {
            Console.WriteLine($"\n[ReservationService] Duke anuluar rezervimin ID: {reservationId}");

            var reservation = await _reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
                throw new InvalidOperationException($"Rezervimi me ID {reservationId} nuk u gjet");

            if (!reservation.CanBeCancelled())
                throw new InvalidOperationException("Ky rezervim nuk mund të anulohet");

            // Ndrysho statusin në Cancelled
            reservation.Status = ReservationStatus.Cancelled;

            await _reservationRepository.UpdateAsync(reservation);

            // Çliro ulësen në fluturim
            await _flightService.ReleaseSeatsAsync(reservation.FlightId, 1);

            Console.WriteLine($"[ReservationService] Rezervimi {reservation.ReservationCode} u anulua me sukses!");
        }

        /// <summary>
        /// Merr të gjitha rezervimet e një pasagjeri
        /// </summary>
        public async Task<IEnumerable<Reservation>> GetPassengerReservationsAsync(int passengerId)
        {
            if (passengerId <= 0)
                throw new ArgumentException("ID e pasagjerit nuk është e vlefshme", nameof(passengerId));

            return await _reservationRepository.GetByPassengerIdAsync(passengerId);
        }

        /// <summary>
        /// Gjeneron një kod unik rezervimi (p.sh. RES-ABC123)
        /// </summary>
        public string GenerateReservationCode()
        {
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var code = "RES-";

            // 3 shkronja të rastësishme
            for (int i = 0; i < 3; i++)
            {
                code += letters[random.Next(letters.Length)];
            }

            // 3 numra të rastësishëm
            code += random.Next(100, 999);

            return code;
        }

        /// <summary>
        /// Gjeneron numër ulëseje bazuar në klasën
        /// </summary>
        private string GenerateSeatNumber(SeatClass seatClass)
        {
            var random = new Random();

            // Rreshti varet nga klasa
            int row = seatClass switch
            {
                SeatClass.FirstClass => random.Next(1, 5),       // Rreshtat 1-4
                SeatClass.Business => random.Next(5, 15),        // Rreshtat 5-14
                SeatClass.PremiumEconomy => random.Next(15, 25), // Rreshtat 15-24
                SeatClass.Economy => random.Next(25, 50),        // Rreshtat 25-49
                _ => random.Next(25, 50)
            };

            // Letra (A-F)
            char letter = (char)('A' + random.Next(0, 6));

            return $"{row}{letter}";
        }
    }
}
