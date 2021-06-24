using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class ProfileModelsUpdate001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "StudentProfileModelId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "StudentIdNumber",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<string>(
                name: "StudentProfileModelUserId",
                table: "StudentSubjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentProfileModelUserId",
                table: "StudentSubjects",
                column: "StudentProfileModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelUserId",
                table: "StudentSubjects",
                column: "StudentProfileModelUserId",
                principalTable: "StudentProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_StudentProfiles_StudentProfileModelUserId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentProfileModelUserId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "StudentProfileModelUserId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<int>(
                name: "StudentProfileModelId",
                table: "StudentSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "StudentIdNumber",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles",
                column: "Id");

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
        }
    }
}
