using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class EditGradesColumnsNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Quizzes",
                newName: "TotalPoints");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "QuizGrades",
                newName: "AssignedGrade");

            migrationBuilder.AddColumn<int>(
                name: "TotalPoints",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "TotalPoints",
                table: "Quizzes",
                newName: "Grade");

            migrationBuilder.RenameColumn(
                name: "AssignedGrade",
                table: "QuizGrades",
                newName: "Grade");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Assignments",
                type: "int",
                nullable: true);
        }
    }
}
