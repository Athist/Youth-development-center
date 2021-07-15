using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class NewSignupAndUodate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Signup");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Signup");

            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "Signup");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "StudentProfiles",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Signup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Signup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "Signup",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
