using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class ProfileModelUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelUserId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectDetailsId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectDetailsId",
                table: "StudentSubjects",
                newName: "SubjectsId");

            migrationBuilder.RenameColumn(
                name: "StudentProfileModelUserId",
                table: "StudentSubjects",
                newName: "StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_SubjectDetailsId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_SubjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudentProfileModelUserId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudentUserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentUserId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectsId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "StudentSubjects",
                newName: "SubjectDetailsId");

            migrationBuilder.RenameColumn(
                name: "StudentUserId",
                table: "StudentSubjects",
                newName: "StudentProfileModelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_SubjectsId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_SubjectDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudentUserId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudentProfileModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelUserId",
                table: "StudentSubjects",
                column: "StudentProfileModelUserId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectDetailsId",
                table: "StudentSubjects",
                column: "SubjectDetailsId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
