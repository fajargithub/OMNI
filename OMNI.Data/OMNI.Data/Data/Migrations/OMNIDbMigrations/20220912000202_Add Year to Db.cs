using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class AddYeartoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "RekomendasiPersonil",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "RekomendasiLatihan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "RekomendasiJenis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "LLPTrx",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "RekomendasiPersonil");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "RekomendasiLatihan");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "RekomendasiJenis");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "LLPTrx");
        }
    }
}
