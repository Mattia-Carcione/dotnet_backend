using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class OrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2024, 8, 29, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9917));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDate",
                value: new DateTime(2024, 8, 24, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookingDate",
                value: new DateTime(2024, 8, 19, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9928));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "BookingDate",
                value: new DateTime(2024, 8, 27, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 8, 14, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9934), new DateTime(2024, 8, 24, 18, 45, 47, 540, DateTimeKind.Local).AddTicks(9935) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BookId", "CreatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 },
                    { 2, 2, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BookId",
                table: "Orders",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2024, 8, 29, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDate",
                value: new DateTime(2024, 8, 24, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1222));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookingDate",
                value: new DateTime(2024, 8, 19, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1224));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "BookingDate",
                value: new DateTime(2024, 8, 27, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1226));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 8, 14, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1228), new DateTime(2024, 8, 24, 16, 56, 7, 406, DateTimeKind.Local).AddTicks(1230) });
        }
    }
}
