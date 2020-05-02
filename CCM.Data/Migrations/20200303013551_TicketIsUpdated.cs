using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class TicketIsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatArrangements");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TicketId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalSeats = table.Column<int>(nullable: false),
                    SoldAmount = table.Column<double>(nullable: false),
                    SoldQuantity = table.Column<int>(nullable: false),
                    TheatreSessionId = table.Column<int>(nullable: false),
                    HasDiscount = table.Column<bool>(nullable: false),
                    TicketType = table.Column<int>(nullable: false),
                    IssuedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_TheatreSessions_TheatreSessionId",
                        column: x => x.TheatreSessionId,
                        principalTable: "TheatreSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TheatreSessionId",
                table: "Tickets",
                column: "TheatreSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.CreateTable(
                name: "SeatArrangements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasDiscount = table.Column<bool>(type: "bit", nullable: false),
                    InternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoldAmount = table.Column<double>(type: "float", nullable: false),
                    SoldQuantity = table.Column<int>(type: "int", nullable: false),
                    TheatreSessionId = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatArrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatArrangements_TheatreSessions_TheatreSessionId",
                        column: x => x.TheatreSessionId,
                        principalTable: "TheatreSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatArrangements_TheatreSessionId",
                table: "SeatArrangements",
                column: "TheatreSessionId");
        }
    }
}
