using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class PrfilwUpde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "StudentProfiles");
        }
    }
}
