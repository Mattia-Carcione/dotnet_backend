using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPremium = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDate", "UserId" },
                values: new object[] { new DateTime(2024, 8, 29, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1088), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDate", "UserId" },
                values: new object[] { new DateTime(2024, 8, 24, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1222), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingDate", "UserId" },
                values: new object[] { new DateTime(2024, 8, 19, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1224), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookingDate", "UserId" },
                values: new object[] { new DateTime(2024, 8, 27, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1226), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "ReturnDate", "UserId" },
                values: new object[] { new DateTime(2024, 8, 14, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1228), new DateTime(2024, 8, 24, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1230), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsPremium", "Username" },
                values: new object[,]
                {
                    { 1, "AliceSmith@email.com", true, "alice" },
                    { 2, "BobSmith@email.com", false, "bob" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDate", "User" },
                values: new object[] { new DateTime(2024, 8, 28, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(2957), "User1" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDate", "User" },
                values: new object[] { new DateTime(2024, 8, 23, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(3030), "User1" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingDate", "User" },
                values: new object[] { new DateTime(2024, 8, 18, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(3033), "User1" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookingDate", "User" },
                values: new object[] { new DateTime(2024, 8, 26, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(3037), "User2" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "ReturnDate", "User" },
                values: new object[] { new DateTime(2024, 8, 13, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(3040), new DateTime(2024, 8, 23, 0, 20, 55, 665, DateTimeKind.Local).AddTicks(3042), "User3" });
        }
    }
}
