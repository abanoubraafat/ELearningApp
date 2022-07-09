using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class MoreContentEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Content",
                newName: "VideoPath");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfPath",
                table: "Content",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "PdfPath",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "VideoPath",
                table: "Content",
                newName: "Path");
        }
    }
}
