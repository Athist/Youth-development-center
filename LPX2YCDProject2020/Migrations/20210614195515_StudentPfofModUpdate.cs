using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class StudentPfofModUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_studentsId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "studentsId",
                table: "StudentSubjects",
                newName: "studentIdId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_studentsId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_studentIdId");

            migrationBuilder.AddColumn<string>(
                name: "studentId",
                table: "StudentProfiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_studentId",
                table: "StudentProfiles",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_AspNetUsers_studentId",
                table: "StudentProfiles",
                column: "studentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Signup_studentIdId",
                table: "StudentSubjects",
                column: "studentIdId",
                principalTable: "Signup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_AspNetUsers_studentId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Signup_studentIdId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_studentId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "StudentProfiles");

            migrationBuilder.RenameColumn(
                name: "studentIdId",
                table: "StudentSubjects",
                newName: "studentsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_studentIdId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_studentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_studentsId",
                table: "StudentSubjects",
                column: "studentsId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
