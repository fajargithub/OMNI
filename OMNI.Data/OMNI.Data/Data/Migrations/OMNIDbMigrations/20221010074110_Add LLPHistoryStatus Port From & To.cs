using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class AddLLPHistoryStatusPortFromTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "LLPHistoryStatus");

            migrationBuilder.AddColumn<string>(
                name: "PortFrom",
                table: "LLPHistoryStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortTo",
                table: "LLPHistoryStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortFrom",
                table: "LLPHistoryStatus");

            migrationBuilder.DropColumn(
                name: "PortTo",
                table: "LLPHistoryStatus");

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "LLPHistoryStatus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
