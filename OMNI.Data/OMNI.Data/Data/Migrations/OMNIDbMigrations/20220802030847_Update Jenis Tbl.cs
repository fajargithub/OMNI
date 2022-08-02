using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateJenisTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryNumber",
                table: "Jenis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kode",
                table: "Jenis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryNumber",
                table: "Jenis");

            migrationBuilder.DropColumn(
                name: "Kode",
                table: "Jenis");
        }
    }
}
