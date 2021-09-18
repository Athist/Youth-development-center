using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class OneMoreTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRsvp");

            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Programmes",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "EventReservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    programmeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_EventReservations_Programmes_programmeId",
                        column: x => x.programmeId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EventReservations_StudentProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventReservations_programmeId",
                table: "EventReservations",
                column: "programmeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReservations_UserId",
                table: "EventReservations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventReservations");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Programmes",
                newName: "Decription");

            migrationBuilder.CreateTable(
                name: "EventRsvp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attended = table.Column<bool>(type: "bit", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    indefined = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    programsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRsvp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRsvp_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRsvp_Programmes_programsId",
                        column: x => x.programsId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRsvp_programsId",
                table: "EventRsvp",
                column: "programsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRsvp_UserId",
                table: "EventRsvp",
                column: "UserId");
        }
    }
}
