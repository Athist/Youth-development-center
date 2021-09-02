using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class ContactUsRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUsFormModel",
                table: "ContactUsFormModel");

            migrationBuilder.RenameTable(
                name: "ContactUsFormModel",
                newName: "Enquiries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enquiries",
                table: "Enquiries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Enquiries",
                table: "Enquiries");

            migrationBuilder.RenameTable(
                name: "Enquiries",
                newName: "ContactUsFormModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUsFormModel",
                table: "ContactUsFormModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactUsFormModelId = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatronType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuburbId = table.Column<int>(type: "int", nullable: true),
                    SuburbName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemDetailsModelId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_ContactUsFormModel_ContactUsFormModelId",
                        column: x => x.ContactUsFormModelId,
                        principalTable: "ContactUsFormModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUs_Suburbs_SuburbId",
                        column: x => x.SuburbId,
                        principalTable: "Suburbs",
                        principalColumn: "SuburbId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUs_SystemDetails_SystemDetailsModelId",
                        column: x => x.SystemDetailsModelId,
                        principalTable: "SystemDetails",
                        principalColumn: "SystemDetailsModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_ContactUsFormModelId",
                table: "ContactUs",
                column: "ContactUsFormModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_SuburbId",
                table: "ContactUs",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_SystemDetailsModelId",
                table: "ContactUs",
                column: "SystemDetailsModelId");
        }
    }
}
