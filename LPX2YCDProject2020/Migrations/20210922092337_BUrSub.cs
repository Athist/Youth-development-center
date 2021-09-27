using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class BUrSub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequiredSubjects",
                columns: table => new
                {
                    BursaryId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectDetailsId = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredSubjects", x => new { x.BursaryId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_RequiredSubjects_Bursaries_BursaryId",
                        column: x => x.BursaryId,
                        principalTable: "Bursaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredSubjects_Subject_SubjectDetailsId",
                        column: x => x.SubjectDetailsId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredSubjects_SubjectDetailsId",
                table: "RequiredSubjects",
                column: "SubjectDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequiredSubjects");
        }
    }
}
