using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Categories_CategoriesID",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Editors_EditorID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Editors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EditorID",
                table: "Books",
                newName: "EditorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_EditorID",
                table: "Books",
                newName: "IX_Books_EditorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoriesID",
                table: "BookCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriesID",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Editors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Editors_EditorId",
                table: "Books",
                column: "EditorId",
                principalTable: "Editors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Editors_EditorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Editors",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "EditorId",
                table: "Books",
                newName: "EditorID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_EditorId",
                table: "Books",
                newName: "IX_Books_EditorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "BookCategory",
                newName: "CategoriesID");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriesId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriesID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Editors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Categories_CategoriesID",
                table: "BookCategory",
                column: "CategoriesID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Editors_EditorID",
                table: "Books",
                column: "EditorID",
                principalTable: "Editors",
                principalColumn: "ID");
        }
    }
}
