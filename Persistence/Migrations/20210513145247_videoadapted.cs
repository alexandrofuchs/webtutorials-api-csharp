using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class videoadapted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_DESCRIPTION",
                table: "VIDEO");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VIDEO");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "VIDEO");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "VIDEO");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "VIDEO",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "VIDEO");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VIDEO",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "VIDEO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "VIDEO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UK_DESCRIPTION",
                table: "VIDEO",
                column: "Description",
                unique: true);
        }
    }
}
