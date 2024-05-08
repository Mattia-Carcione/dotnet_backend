using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class mid3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_Book_BookedBookId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Booking_Date",
                table: "Reservations",
                newName: "Reservation_Date");

            migrationBuilder.RenameColumn(
                name: "Book_BookedBookId",
                table: "Reservations",
                newName: "Book_ReservedBookId");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Reservations",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Book_BookedBookId",
                table: "Reservations",
                newName: "IX_Reservations_Book_ReservedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_Book_ReservedBookId",
                table: "Reservations",
                column: "Book_ReservedBookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_Book_ReservedBookId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Reservation_Date",
                table: "Reservations",
                newName: "Booking_Date");

            migrationBuilder.RenameColumn(
                name: "Book_ReservedBookId",
                table: "Reservations",
                newName: "Book_BookedBookId");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reservations",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Book_ReservedBookId",
                table: "Reservations",
                newName: "IX_Reservations_Book_BookedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_Book_BookedBookId",
                table: "Reservations",
                column: "Book_BookedBookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
