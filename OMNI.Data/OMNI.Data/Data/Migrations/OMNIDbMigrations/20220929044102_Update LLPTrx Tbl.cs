using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateLLPTrxTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "LLPTrx",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "LLPTrx");
        }
    }
}
