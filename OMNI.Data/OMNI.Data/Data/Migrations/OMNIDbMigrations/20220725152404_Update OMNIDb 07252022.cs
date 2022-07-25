using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Migrations.Data.Migrations.OMNIDbMigrations
{
    public partial class UpdateOMNIDb07252022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "RekomendasiHubla",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "SisaMasaBerlaki",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "Personil");

            migrationBuilder.DropColumn(
                name: "KesesuaianPM58",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "PersentaseHubla",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "RekomendasiOSCP",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "LatihanTrx");

            migrationBuilder.DropColumn(
                name: "Satuan",
                table: "LatihanTrx");

            migrationBuilder.DropColumn(
                name: "PortId",
                table: "Latihan");

            migrationBuilder.RenameColumn(
                name: "TOtalKebutuhanHubla",
                table: "LLPTrx",
                newName: "TotalKebutuhanHubla");

            migrationBuilder.AddColumn<int>(
                name: "JenisId",
                table: "SpesifikasiJenis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "PersonilTrx",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SisaMasaBerlaku",
                table: "PersonilTrx",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KesesuaianMP58",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "LLPTrx",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalExistingKeseluruhan",
                table: "LLPTrx",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "LatihanTrx",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryLatihanTrx",
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
                    LatihanId = table.Column<int>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    TanggalPelaksanaan = table.Column<DateTime>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianPM58 = table.Column<string>(nullable: true),
                    PersentaseLatihan = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryLatihanTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryLatihanTrx_Latihan_LatihanId",
                        column: x => x.LatihanId,
                        principalTable: "Latihan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryLLPTrx",
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
                    SpesifikasiJenisId = table.Column<int>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    QRCode = table.Column<string>(nullable: true),
                    DetailExisting = table.Column<float>(nullable: false),
                    Kondisi = table.Column<string>(nullable: true),
                    TotalExistingJenis = table.Column<float>(nullable: false),
                    TotalExistingKeseluruhan = table.Column<float>(nullable: false),
                    TotalKebutuhanHubla = table.Column<float>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianMP58 = table.Column<string>(nullable: true),
                    TotalKebutuhanOSCP = table.Column<float>(nullable: false),
                    SelisihOSCP = table.Column<float>(nullable: false),
                    KesesuaianOSCP = table.Column<string>(nullable: true),
                    PersentaseOSCP = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryLLPTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryLLPTrx_SpesifikasiJenis_SpesifikasiJenisId",
                        column: x => x.SpesifikasiJenisId,
                        principalTable: "SpesifikasiJenis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryPersonilTrx",
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
                    PersonilId = table.Column<int>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalDetailExisting = table.Column<float>(nullable: false),
                    TanggalPelatihan = table.Column<DateTime>(nullable: false),
                    TanggalExpired = table.Column<DateTime>(nullable: false),
                    SisaMasaBerlaku = table.Column<int>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianPM58 = table.Column<string>(nullable: true),
                    PersentasePersonil = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPersonilTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryPersonilTrx_Personil_PersonilId",
                        column: x => x.PersonilId,
                        principalTable: "Personil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jenis",
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
                    Name = table.Column<string>(nullable: true),
                    Satuan = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jenis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kondisi",
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
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kondisi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RekomendasiType",
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
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekomendasiType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RekomendasiJenis",
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
                    Port = table.Column<string>(nullable: true),
                    SpesifikasiJenisId = table.Column<int>(nullable: true),
                    RekomendasiTypeId = table.Column<int>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekomendasiJenis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RekomendasiJenis_RekomendasiType_RekomendasiTypeId",
                        column: x => x.RekomendasiTypeId,
                        principalTable: "RekomendasiType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RekomendasiJenis_SpesifikasiJenis_SpesifikasiJenisId",
                        column: x => x.SpesifikasiJenisId,
                        principalTable: "SpesifikasiJenis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RekomendasiLatihan",
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
                    Port = table.Column<string>(nullable: true),
                    LatihanId = table.Column<int>(nullable: true),
                    RekomendasiTypeId = table.Column<int>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekomendasiLatihan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RekomendasiLatihan_Latihan_LatihanId",
                        column: x => x.LatihanId,
                        principalTable: "Latihan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RekomendasiLatihan_RekomendasiType_RekomendasiTypeId",
                        column: x => x.RekomendasiTypeId,
                        principalTable: "RekomendasiType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RekomendasiPersonil",
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
                    Port = table.Column<string>(nullable: true),
                    PersonilId = table.Column<int>(nullable: true),
                    RekomendasiTypeId = table.Column<int>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekomendasiPersonil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RekomendasiPersonil_Personil_PersonilId",
                        column: x => x.PersonilId,
                        principalTable: "Personil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RekomendasiPersonil_RekomendasiType_RekomendasiTypeId",
                        column: x => x.RekomendasiTypeId,
                        principalTable: "RekomendasiType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpesifikasiJenis_JenisId",
                table: "SpesifikasiJenis",
                column: "JenisId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLatihanTrx_LatihanId",
                table: "HistoryLatihanTrx",
                column: "LatihanId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLLPTrx_SpesifikasiJenisId",
                table: "HistoryLLPTrx",
                column: "SpesifikasiJenisId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPersonilTrx_PersonilId",
                table: "HistoryPersonilTrx",
                column: "PersonilId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiJenis_RekomendasiTypeId",
                table: "RekomendasiJenis",
                column: "RekomendasiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiJenis_SpesifikasiJenisId",
                table: "RekomendasiJenis",
                column: "SpesifikasiJenisId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiLatihan_LatihanId",
                table: "RekomendasiLatihan",
                column: "LatihanId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiLatihan_RekomendasiTypeId",
                table: "RekomendasiLatihan",
                column: "RekomendasiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiPersonil_PersonilId",
                table: "RekomendasiPersonil",
                column: "PersonilId");

            migrationBuilder.CreateIndex(
                name: "IX_RekomendasiPersonil_RekomendasiTypeId",
                table: "RekomendasiPersonil",
                column: "RekomendasiTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpesifikasiJenis_Jenis_JenisId",
                table: "SpesifikasiJenis",
                column: "JenisId",
                principalTable: "Jenis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpesifikasiJenis_Jenis_JenisId",
                table: "SpesifikasiJenis");

            migrationBuilder.DropTable(
                name: "HistoryLatihanTrx");

            migrationBuilder.DropTable(
                name: "HistoryLLPTrx");

            migrationBuilder.DropTable(
                name: "HistoryPersonilTrx");

            migrationBuilder.DropTable(
                name: "Jenis");

            migrationBuilder.DropTable(
                name: "Kondisi");

            migrationBuilder.DropTable(
                name: "RekomendasiJenis");

            migrationBuilder.DropTable(
                name: "RekomendasiLatihan");

            migrationBuilder.DropTable(
                name: "RekomendasiPersonil");

            migrationBuilder.DropTable(
                name: "RekomendasiType");

            migrationBuilder.DropIndex(
                name: "IX_SpesifikasiJenis_JenisId",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "JenisId",
                table: "SpesifikasiJenis");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "SisaMasaBerlaku",
                table: "PersonilTrx");

            migrationBuilder.DropColumn(
                name: "KesesuaianMP58",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "QRCode",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "TotalExistingKeseluruhan",
                table: "LLPTrx");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "LatihanTrx");

            migrationBuilder.RenameColumn(
                name: "TotalKebutuhanHubla",
                table: "LLPTrx",
                newName: "TOtalKebutuhanHubla");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "SpesifikasiJenis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SpesifikasiJenis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "SpesifikasiJenis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QRCode",
                table: "SpesifikasiJenis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RekomendasiHubla",
                table: "SpesifikasiJenis",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "PersonilTrx",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "PersonilTrx",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SisaMasaBerlaki",
                table: "PersonilTrx",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "Personil",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KesesuaianPM58",
                table: "LLPTrx",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PersentaseHubla",
                table: "LLPTrx",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "LLPTrx",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "RekomendasiOSCP",
                table: "LLPTrx",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "LLPTrx",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "LatihanTrx",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Satuan",
                table: "LatihanTrx",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortId",
                table: "Latihan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
