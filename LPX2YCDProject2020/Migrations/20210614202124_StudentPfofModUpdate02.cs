using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class StudentPfofModUpdate02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Signup_studentIdId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_studentIdId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "studentIdId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "thirdTermMark",
                table: "StudentSubjects",
                newName: "ThirdTermMark");

            migrationBuilder.AddColumn<string>(
                name: "StudentIdNumber",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentIdNumber",
                table: "StudentProfiles");

            migrationBuilder.RenameColumn(
                name: "ThirdTermMark",
                table: "StudentSubjects",
                newName: "thirdTermMark");

            migrationBuilder.AddColumn<int>(
                name: "studentIdId",
                table: "StudentSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_studentIdId",
                table: "StudentSubjects",
                column: "studentIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Signup_studentIdId",
                table: "StudentSubjects",
                column: "studentIdId",
                principalTable: "Signup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
