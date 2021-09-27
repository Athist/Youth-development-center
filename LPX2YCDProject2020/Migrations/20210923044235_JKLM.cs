using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class JKLM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateIndex(
                name: "IX_SubjectRequirement_SubjectId",
                table: "SubjectRequirement",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectRequirement_Subject_SubjectId",
                table: "SubjectRequirement",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectRequirement_Subject_SubjectId",
                table: "SubjectRequirement");

            migrationBuilder.DropIndex(
                name: "IX_SubjectRequirement_SubjectId",
                table: "SubjectRequirement");

            migrationBuilder.AddColumn<int>(
                name: "SubjectDetailsId",
                table: "SubjectRequirement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectRequirement_SubjectDetailsId",
                table: "SubjectRequirement",
                column: "SubjectDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectRequirement_Subject_SubjectDetailsId",
                table: "SubjectRequirement",
                column: "SubjectDetailsId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
