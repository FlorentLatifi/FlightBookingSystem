using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Text.RegularExpressions;

namespace FlightBooking.Domain.ValueObjects
{
    public class SeatNumber : IEquatable<SeatNumber>
    {
        public string Row { get; }
        public string Letter { get; }
        public string FullSeat => $"{Row}{Letter}";

        public SeatNumber(string row, string letter)
        {
            if (string.IsNullOrWhiteSpace(row))
                throw new ArgumentException("Row cannot be empty");

            if (string.IsNullOrWhiteSpace(letter) || letter.Length != 1)
                throw new ArgumentException("Letter must be a single character");

            Row = row;
            Letter = letter.ToUpper();
        }

        public static SeatNumber Parse(string seatNumber)
        {
            var match = Regex.Match(seatNumber, @"^(\d+)([A-Z])$", RegexOptions.IgnoreCase);

            if (!match.Success)
                throw new ArgumentException($"Invalid seat number format: {seatNumber}");

            return new SeatNumber(match.Groups[1].Value, match.Groups[2].Value);
        }

        public bool Equals(SeatNumber? other)
        {
            if (other is null) return false;
            return Row == other.Row && Letter == other.Letter;
        }

        public override bool Equals(object? obj) => Equals(obj as SeatNumber);

        public override int GetHashCode() => HashCode.Combine(Row, Letter);

        public override string ToString() => FullSeat;
    }
}