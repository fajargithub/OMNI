using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    public partial class AddActivityLogtoOMNIDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OMNIActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 400, nullable: true),
                    IsDeleted = table.Column<string>(maxLength: 1, nullable: true),
                    IsActive = table.Column<string>(maxLength: 1, nullable: true),
                    TrxId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Controller = table.Column<string>(maxLength: 40, nullable: false),
                    Method = table.Column<string>(maxLength: 40, nullable: true),
                    Status = table.Column<string>(maxLength: 10, nullable: false),
                    Info = table.Column<string>(maxLength: 400, nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OMNIActivityLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OMNIActivityLog");
        }
    }
}
