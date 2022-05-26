using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class QuizEditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizGrades_QuizAnswers_QuizAnswerId",
                table: "QuizGrades");

            migrationBuilder.DropTable(
                name: "QuizAnswers");

            migrationBuilder.DropColumn(
                name: "QuizFile",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "modelAnswer",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer2",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer3",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer4",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FirstChoise",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FourthChoise",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Lastchoise",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ThirdChoise",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "State",
                table: "QuestionAnswers");

            migrationBuilder.RenameColumn(
                name: "QuizAnswerId",
                table: "QuizGrades",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizGrades_QuizAnswerId",
                table: "QuizGrades",
                newName: "IX_QuizGrades_StudentId");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswer",
                table: "Questions",
                newName: "correctAnswer");

            migrationBuilder.RenameColumn(
                name: "SecondChoise",
                table: "Questions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Questions",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_LessonId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.RenameColumn(
                name: "QuestionAnswerText",
                table: "QuestionAnswers",
                newName: "Answer");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostTime",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "QuizGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDate",
                table: "Questions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_QuizGrades_QuizId",
                table: "QuizGrades",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGrades_Quizzes_QuizId",
                table: "QuizGrades",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGrades_Students_StudentId",
                table: "QuizGrades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizGrades_Quizzes_QuizId",
                table: "QuizGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizGrades_Students_StudentId",
                table: "QuizGrades");

            migrationBuilder.DropIndex(
                name: "IX_QuizGrades_QuizId",
                table: "QuizGrades");

            migrationBuilder.DropColumn(
                name: "PostTime",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "QuizGrades");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "QuizGrades",
                newName: "QuizAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizGrades_StudentId",
                table: "QuizGrades",
                newName: "IX_QuizGrades_QuizAnswerId");

            migrationBuilder.RenameColumn(
                name: "correctAnswer",
                table: "Questions",
                newName: "CorrectAnswer");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Questions",
                newName: "SecondChoise");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Questions",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                newName: "IX_Questions_LessonId");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "QuestionAnswers",
                newName: "QuestionAnswerText");

            migrationBuilder.AddColumn<string>(
                name: "QuizFile",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "modelAnswer",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDate",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer2",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer3",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer4",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstChoise",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FourthChoise",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastchoise",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThirdChoise",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "QuestionAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "QuizAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswers_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAnswers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_QuizId",
                table: "QuizAnswers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_StudentId",
                table: "QuizAnswers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGrades_QuizAnswers_QuizAnswerId",
                table: "QuizGrades",
                column: "QuizAnswerId",
                principalTable: "QuizAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
