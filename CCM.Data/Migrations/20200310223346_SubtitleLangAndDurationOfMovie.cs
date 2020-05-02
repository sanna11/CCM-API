using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class SubtitleLangAndDurationOfMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreSessions_TheatreHalls_TheatrehallId",
                table: "TheatreSessions");

            migrationBuilder.DropColumn(
                name: "Subtitles",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "TheatrehallId",
                table: "TheatreSessions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "TheatreSessions",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ScreeningDuration",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_TheatreSessions_TheatreId",
                table: "TheatreSessions",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreSessions_Theatres_TheatreId",
                table: "TheatreSessions",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreSessions_TheatreHalls_TheatrehallId",
                table: "TheatreSessions",
                column: "TheatrehallId",
                principalTable: "TheatreHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreSessions_Theatres_TheatreId",
                table: "TheatreSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TheatreSessions_TheatreHalls_TheatrehallId",
                table: "TheatreSessions");

            migrationBuilder.DropIndex(
                name: "IX_TheatreSessions_TheatreId",
                table: "TheatreSessions");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "TheatreSessions");

            migrationBuilder.AlterColumn<int>(
                name: "TheatrehallId",
                table: "TheatreSessions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ScreeningDuration",
                table: "Movies",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subtitles",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreSessions_TheatreHalls_TheatrehallId",
                table: "TheatreSessions",
                column: "TheatrehallId",
                principalTable: "TheatreHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
