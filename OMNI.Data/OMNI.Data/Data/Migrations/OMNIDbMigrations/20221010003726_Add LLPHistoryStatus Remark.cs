using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class AddLLPHistoryStatusRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "LLPHistoryStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "LLPHistoryStatus");
        }
    }
}
