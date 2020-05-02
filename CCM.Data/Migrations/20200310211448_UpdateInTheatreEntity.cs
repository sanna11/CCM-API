using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class UpdateInTheatreEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Theatres",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Theatres",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Theatres");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Theatres");
        }
    }
}
