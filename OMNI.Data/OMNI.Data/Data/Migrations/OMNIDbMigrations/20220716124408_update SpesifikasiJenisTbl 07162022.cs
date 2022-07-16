using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    public partial class updateSpesifikasiJenisTbl07162022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "SpesifikasiJenis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "SpesifikasiJenis",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RekomendasiHubla",
                table: "SpesifikasiJenis",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortId",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "RekomendasiHubla",
                table: "SpesifikasiJenis");
        }
    }
}
