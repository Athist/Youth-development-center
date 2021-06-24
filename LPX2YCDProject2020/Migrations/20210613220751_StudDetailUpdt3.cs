using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class StudDetailUpdt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "CurrentPercentage",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstTermMark",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondTermMark",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thirdTermMark",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "FirstTermMark",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "SecondTermMark",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "thirdTermMark",
                table: "StudentSubjects");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentPercentage",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
