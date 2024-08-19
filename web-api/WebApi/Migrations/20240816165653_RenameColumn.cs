using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Books_BookingId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Bookings",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingId",
                table: "Bookings",
                newName: "IX_Bookings_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Books_BookId",
                table: "Bookings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Books_BookId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Bookings",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookId",
                table: "Bookings",
                newName: "IX_Bookings_BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Books_BookingId",
                table: "Bookings",
                column: "BookingId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
