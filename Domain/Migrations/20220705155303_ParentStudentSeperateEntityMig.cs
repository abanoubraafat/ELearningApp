using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class ParentStudentSeperateEntityMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Students_StudentsId",
                table: "ParentStudent");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "ParentStudent",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "ParentsId",
                table: "ParentStudent",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudent_StudentsId",
                table: "ParentStudent",
                newName: "IX_ParentStudent_StudentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "ParentStudent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parents_ParentId",
                table: "ParentStudent",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Students_StudentId",
                table: "ParentStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parents_ParentId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Students_StudentId",
                table: "ParentStudent");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "ParentStudent");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ParentStudent",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ParentStudent",
                newName: "ParentsId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudent_StudentId",
                table: "ParentStudent",
                newName: "IX_ParentStudent_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Students_StudentsId",
                table: "ParentStudent",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
