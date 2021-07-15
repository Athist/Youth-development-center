using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class YearAddedToStudentProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "StudentSubjects");
        }
    }
}
