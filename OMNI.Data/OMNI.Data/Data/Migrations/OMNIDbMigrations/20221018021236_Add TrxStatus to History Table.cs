using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class AddTrxStatustoHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrxStatus",
                table: "HistoryPersonilTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrxStatus",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrxStatus",
                table: "HistoryLatihanTrx",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrxStatus",
                table: "HistoryPersonilTrx");

            migrationBuilder.DropColumn(
                name: "TrxStatus",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "TrxStatus",
                table: "HistoryLatihanTrx");
        }
    }
}
