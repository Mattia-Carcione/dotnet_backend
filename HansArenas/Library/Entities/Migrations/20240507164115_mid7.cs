using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class mid7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_Book_ReservedBookId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Book_ReservedBookId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Book_ReservedBookId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Book_Id",
                table: "Reservations",
                newName: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Books_BookId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Reservations",
                newName: "Book_Id");

            migrationBuilder.AddColumn<int>(
                name: "Book_ReservedBookId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Book_ReservedBookId",
                table: "Reservations",
                column: "Book_ReservedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Books_Book_ReservedBookId",
                table: "Reservations",
                column: "Book_ReservedBookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
