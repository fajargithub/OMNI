using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateOMNIDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonilTrx_Latihan_LatihanId",
                table: "PersonilTrx");

            migrationBuilder.DropTable(
                name: "DetailLatihan");

            migrationBuilder.DropTable(
                name: "DetailPersonil");

            migrationBuilder.DropTable(
                name: "DetailSpesifikasi");

            migrationBuilder.DropIndex(
                name: "IX_PersonilTrx_LatihanId",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "LatihanId",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "LLPTrx");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalPelatihan",
                table: "PersonilTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalExpired",
                table: "PersonilTrx",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PersonilTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonilId",
                table: "PersonilTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "Personil",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Personil",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "Latihan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Latihan",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_PersonilTrx_PersonilId",
                table: "PersonilTrx",
                column: "PersonilId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonilTrx_Personil_PersonilId",
                table: "PersonilTrx",
                column: "PersonilId",
                principalTable: "Personil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonilTrx_Personil_PersonilId",
                table: "PersonilTrx");

            migrationBuilder.DropIndex(
                name: "IX_PersonilTrx_PersonilId",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "PersonilId",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "Personil");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Personil");

            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "Latihan");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Latihan");

            migrationBuilder.AlterColumn<float>(
                name: "TanggalPelatihan",
                table: "PersonilTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<float>(
                name: "TanggalExpired",
                table: "PersonilTrx",
                type: "real",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "LatihanId",
                table: "PersonilTrx",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UOMId",
                table: "LLPTrx",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DetailLatihan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    LatihanId = table.Column<int>(type: "int", nullable: true),
                    PortId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailLatihan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailLatihan_Latihan_LatihanId",
                        column: x => x.LatihanId,
                        principalTable: "Latihan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailPersonil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    PersonilId = table.Column<int>(type: "int", nullable: true),
                    PortId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPersonil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailPersonil_Personil_PersonilId",
                        column: x => x.PersonilId,
                        principalTable: "Personil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailSpesifikasi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortId = table.Column<int>(type: "int", nullable: false),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RekomendasiHubla = table.Column<float>(type: "real", nullable: false),
                    SpesifikasiJenisId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailSpesifikasi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailSpesifikasi_SpesifikasiJenis_SpesifikasiJenisId",
                        column: x => x.SpesifikasiJenisId,
                        principalTable: "SpesifikasiJenis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonilTrx_LatihanId",
                table: "PersonilTrx",
                column: "LatihanId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailLatihan_LatihanId",
                table: "DetailLatihan",
                column: "LatihanId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPersonil_PersonilId",
                table: "DetailPersonil",
                column: "PersonilId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailSpesifikasi_SpesifikasiJenisId",
                table: "DetailSpesifikasi",
                column: "SpesifikasiJenisId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonilTrx_Latihan_LatihanId",
                table: "PersonilTrx",
                column: "LatihanId",
                principalTable: "Latihan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
