using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BooksID",
                table: "BookCategory");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BooksID",
                table: "BookCategory",
                newName: "BooksId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Authors",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Austen", "Jane" },
                    { 2, new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Twain", "Mark" },
                    { 3, new DateTime(1812, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dickens", "Charles" },
                    { 4, new DateTime(1797, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shelley", "Mary" },
                    { 5, new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orwell", "George" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Genre" },
                values: new object[,]
                {
                    { 1, "Fictional books", "Fiction" },
                    { 2, "Sci-Fi books", "Science Fiction" },
                    { 3, "Mystery and detective stories", "Mystery" },
                    { 4, "Biographical works", "Biography" },
                    { 5, "Horror and thriller books", "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Editors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Penguin Books" },
                    { 2, "HarperCollins" },
                    { 3, "Random House" },
                    { 4, "Simon & Schuster" },
                    { 5, "Macmillan Publishers" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Copies", "EditorId", "Pages", "PublicationDate", "Title", "TotalCopies" },
                values: new object[,]
                {
                    { 1, 1, 5, 1, 432, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pride and Prejudice", 10 },
                    { 2, 2, 7, 2, 366, new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventures of Huckleberry Finn", 15 },
                    { 3, 3, 8, 3, 544, new DateTime(1861, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Great Expectations", 12 },
                    { 4, 4, 3, 4, 280, new DateTime(1818, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frankenstein", 8 },
                    { 5, 5, 10, 5, 328, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984", 20 }
                });

            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookId", "BookingDate", "DeliveryDate", "User" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 12, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5432), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User1" },
                    { 2, 2, new DateTime(2024, 8, 7, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5474), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User1" },
                    { 3, 3, new DateTime(2024, 8, 2, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5476), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User1" },
                    { 4, 4, new DateTime(2024, 8, 10, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5478), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User2" },
                    { 5, 5, new DateTime(2024, 7, 28, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5479), new DateTime(2024, 8, 7, 18, 19, 17, 763, DateTimeKind.Local).AddTicks(5480), "User3" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory");

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "BookCategory",
                keyColumns: new[] { "BooksId", "CategoriesId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Editors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "BookCategory",
                newName: "BooksID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BooksID",
                table: "BookCategory",
                column: "BooksID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
