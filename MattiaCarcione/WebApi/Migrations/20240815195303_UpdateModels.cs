using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookings_BookingID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookingID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookID",
                table: "Bookings",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Books_BookID",
                table: "Bookings",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Books_BookID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingID",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookingID",
                table: "Books",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookings_BookingID",
                table: "Books",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "ID");
        }
    }
}
