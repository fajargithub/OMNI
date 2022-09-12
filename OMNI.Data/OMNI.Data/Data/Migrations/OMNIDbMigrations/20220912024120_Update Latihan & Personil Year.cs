using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateLatihanPersonilYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "PersonilTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "LatihanTrx",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "LatihanTrx");
        }
    }
}
