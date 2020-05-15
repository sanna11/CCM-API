using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class TicketSaleAndTicketUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TheatreSessions_TheatreSessionId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TheatreSessionId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IssuedTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SoldAmount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SoldQuantity",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TheatreSessionId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "Tickets");

            migrationBuilder.AddColumn<double>(
                name: "TicketPrice",
                table: "Tickets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TicketSaleId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TicketSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TotalSeats = table.Column<int>(nullable: false),
                    SoldAmount = table.Column<double>(nullable: false),
                    TheatreSessionId = table.Column<int>(nullable: false),
                    HasDiscount = table.Column<bool>(nullable: false),
                    DiscountValue = table.Column<double>(nullable: false),
                    IssuedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSales_TheatreSessions_TheatreSessionId",
                        column: x => x.TheatreSessionId,
                        principalTable: "TheatreSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketSaleId",
                table: "Tickets",
                column: "TicketSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSales_TheatreSessionId",
                table: "TicketSales",
                column: "TheatreSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSales_TicketSaleId",
                table: "Tickets",
                column: "TicketSaleId",
                principalTable: "TicketSales",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSales_TicketSaleId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketSales");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketSaleId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketSaleId",
                table: "Tickets");

            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedTime",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SoldAmount",
                table: "Tickets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SoldQuantity",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TheatreSessionId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TheatreSessionId",
                table: "Tickets",
                column: "TheatreSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TheatreSessions_TheatreSessionId",
                table: "Tickets",
                column: "TheatreSessionId",
                principalTable: "TheatreSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
