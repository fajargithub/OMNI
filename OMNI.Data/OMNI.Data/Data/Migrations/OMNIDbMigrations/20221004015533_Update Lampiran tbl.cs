using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateLampirantbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lampiran_LLPTrx_LLPTrxId",
                table: "Lampiran");

            migrationBuilder.DropIndex(
                name: "IX_Lampiran_LLPTrxId",
                table: "Lampiran");

            migrationBuilder.DropColumn(
                name: "LLPTrxId",
                table: "Lampiran");

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "Lampiran",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "Lampiran");

            migrationBuilder.AddColumn<int>(
                name: "LLPTrxId",
                table: "Lampiran",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lampiran_LLPTrxId",
                table: "Lampiran",
                column: "LLPTrxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lampiran_LLPTrx_LLPTrxId",
                table: "Lampiran",
                column: "LLPTrxId",
                principalTable: "LLPTrx",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
