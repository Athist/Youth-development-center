using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class AAA1234456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentUserId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectsId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentUserId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubjectsId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectsId",
                table: "StudentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentSubjects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_UserId",
                table: "StudentSubjects",
                column: "UserId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_UserId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId",
                table: "StudentSubjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsId",
                table: "StudentSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentUserId",
                table: "StudentSubjects",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectsId",
                table: "StudentSubjects",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentUserId",
                table: "StudentSubjects",
                column: "StudentUserId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectsId",
                table: "StudentSubjects",
                column: "SubjectsId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
