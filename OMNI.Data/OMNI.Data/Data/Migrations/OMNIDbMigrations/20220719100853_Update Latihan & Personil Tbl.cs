using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateLatihanPersonilTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Personil");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Latihan");

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "Personil",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "Latihan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "Personil");

            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "Latihan");

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Personil",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Latihan",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
