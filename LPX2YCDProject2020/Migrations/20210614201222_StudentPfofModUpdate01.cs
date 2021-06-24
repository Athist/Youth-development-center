using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class StudentPfofModUpdate01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_AspNetUsers_studentId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_studentId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "StudentProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
