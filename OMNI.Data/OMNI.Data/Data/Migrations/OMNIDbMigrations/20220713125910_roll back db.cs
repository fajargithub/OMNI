using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    public partial class rollbackdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_TEST",
                table: "Latihan");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Latihan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Latihan");

            migrationBuilder.AddColumn<string>(
                name: "Name_TEST",
                table: "Latihan",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
