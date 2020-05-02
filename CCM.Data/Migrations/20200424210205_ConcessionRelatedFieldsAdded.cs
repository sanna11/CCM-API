using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class ConcessionRelatedFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ConcessCost",
                table: "TheatreSessions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ConcessSales",
                table: "TheatreSessions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ConcessTrans",
                table: "TheatreSessions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcessCost",
                table: "TheatreSessions");

            migrationBuilder.DropColumn(
                name: "ConcessSales",
                table: "TheatreSessions");

            migrationBuilder.DropColumn(
                name: "ConcessTrans",
                table: "TheatreSessions");
        }
    }
}
