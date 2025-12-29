using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 28, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 28, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 28, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 28, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 28, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 28, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 28, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 28, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 28, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 28, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 29, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 29, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 29, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 29, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 29, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 29, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 30, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 30, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 1, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 1, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 2, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 2, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 3, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 4, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 4, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 5, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 5, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 6, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 6, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 7, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 7, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 8, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 8, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 9, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 9, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 10, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 10, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 11, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 11, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 12, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 12, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 13, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 13, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 14, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 16, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 16, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 17, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 17, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 18, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 19, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 19, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 20, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 20, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 21, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 21, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 22, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 22, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 23, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 24, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 24, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 25, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 26, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 26, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 6, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 16, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 10, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 10, 30, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 7, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2026, 1, 27, 10, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 1, 27, 8, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);
        }
    }
}
