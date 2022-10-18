using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDetailExisting",
                table: "HistoryPersonilTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "SelisihHubla",
                table: "HistoryPersonilTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "PersentasePersonil",
                table: "HistoryPersonilTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "PersonilTrxId",
                table: "HistoryPersonilTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HistoryPersonilTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LLPTrxId",
                table: "HistoryLLPTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoAsset",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersentaseHubla",
                table: "HistoryLLPTrx",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "QRCodeText",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "HistoryLLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HistoryLLPTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "SelisihHubla",
                table: "HistoryLatihanTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "PersentaseLatihan",
                table: "HistoryLatihanTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "LatihanTrxId",
                table: "HistoryLatihanTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HistoryLatihanTrx",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonilTrxId",
                table: "HistoryPersonilTrx");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HistoryPersonilTrx");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "LLPTrxId",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "NoAsset",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "PersentaseHubla",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "QRCodeText",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HistoryLLPTrx");

            migrationBuilder.DropColumn(
                name: "LatihanTrxId",
                table: "HistoryLatihanTrx");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HistoryLatihanTrx");

            migrationBuilder.AlterColumn<float>(
                name: "TotalDetailExisting",
                table: "HistoryPersonilTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "SelisihHubla",
                table: "HistoryPersonilTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "PersentasePersonil",
                table: "HistoryPersonilTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "SelisihHubla",
                table: "HistoryLatihanTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "PersentaseLatihan",
                table: "HistoryLatihanTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
