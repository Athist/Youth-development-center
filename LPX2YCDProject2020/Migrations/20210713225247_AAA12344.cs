using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class AAA12344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudentSubjects",
                newName: "EnrolmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrolmentId",
                table: "StudentSubjects",
                newName: "Id");
        }
    }
}
