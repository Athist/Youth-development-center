using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class AnotherOneOfBCS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredSubjects",
                table: "RequiredSubjects");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RequiredSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredSubjects",
                table: "RequiredSubjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredSubjects_BursaryId",
                table: "RequiredSubjects",
                column: "BursaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredSubjects",
                table: "RequiredSubjects");

            migrationBuilder.DropIndex(
                name: "IX_RequiredSubjects_BursaryId",
                table: "RequiredSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequiredSubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredSubjects",
                table: "RequiredSubjects",
                columns: new[] { "BursaryId", "SubjectId" });
        }
    }
}
