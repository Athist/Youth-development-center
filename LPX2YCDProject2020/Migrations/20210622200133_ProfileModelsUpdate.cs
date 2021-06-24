using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class ProfileModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_subjectDetailsId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "subjectDetailsId",
                table: "StudentSubjects",
                newName: "SubjectDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_subjectDetailsId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_SubjectDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "StudentProfileModelId",
                table: "StudentSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentProfileModelId",
                table: "StudentSubjects",
                column: "StudentProfileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelId",
                table: "StudentSubjects",
                column: "StudentProfileModelId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectDetailsId",
                table: "StudentSubjects",
                column: "SubjectDetailsId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subject_SubjectDetailsId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectDetailsId",
                table: "StudentSubjects",
                newName: "subjectDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_SubjectDetailsId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_subjectDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subject_subjectDetailsId",
                table: "StudentSubjects",
                column: "subjectDetailsId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
