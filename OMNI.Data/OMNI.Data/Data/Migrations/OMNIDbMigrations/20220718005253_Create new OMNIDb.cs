using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    public partial class CreatenewOMNIDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Latihan",
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
                    PortId = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Latihan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeralatanOSR",
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
                    table.PrimaryKey("PK_PeralatanOSR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personil",
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
                    PortId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LatihanTrx",
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
                    PortId = table.Column<int>(nullable: false),
                    Satuan = table.Column<string>(nullable: true),
                    TanggalPelaksanaan = table.Column<DateTime>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianPM58 = table.Column<string>(nullable: true),
                    PersentaseLatihan = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatihanTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LatihanTrx_Latihan_LatihanId",
                        column: x => x.LatihanId,
                        principalTable: "Latihan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpesifikasiJenis",
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
                    PeralatanOSRId = table.Column<int>(nullable: true),
                    PortId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    QRCode = table.Column<string>(nullable: true),
                    RekomendasiHubla = table.Column<float>(nullable: false),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpesifikasiJenis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpesifikasiJenis_PeralatanOSR_PeralatanOSRId",
                        column: x => x.PeralatanOSRId,
                        principalTable: "PeralatanOSR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonilTrx",
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
                    PortId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Satuan = table.Column<string>(nullable: true),
                    TotalDetailExisting = table.Column<float>(nullable: false),
                    TanggalPelatihan = table.Column<DateTime>(nullable: false),
                    TanggalExpired = table.Column<DateTime>(nullable: false),
                    SisaMasaBerlaki = table.Column<int>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianPM58 = table.Column<string>(nullable: true),
                    PersentasePersonil = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonilTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonilTrx_Personil_PersonilId",
                        column: x => x.PersonilId,
                        principalTable: "Personil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LLPTrx",
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
                    PortId = table.Column<int>(nullable: false),
                    Satuan = table.Column<string>(nullable: true),
                    DetailExisting = table.Column<float>(nullable: false),
                    Kondisi = table.Column<string>(nullable: true),
                    RekomendasiOSCP = table.Column<float>(nullable: false),
                    TotalExistingJenis = table.Column<float>(nullable: false),
                    TOtalKebutuhanHubla = table.Column<float>(nullable: false),
                    SelisihHubla = table.Column<float>(nullable: false),
                    KesesuaianPM58 = table.Column<string>(nullable: true),
                    PersentaseHubla = table.Column<float>(nullable: false),
                    TotalKebutuhanOSCP = table.Column<float>(nullable: false),
                    SelisihOSCP = table.Column<float>(nullable: false),
                    KesesuaianOSCP = table.Column<string>(nullable: true),
                    PersentaseOSCP = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLPTrx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLPTrx_SpesifikasiJenis_SpesifikasiJenisId",
                        column: x => x.SpesifikasiJenisId,
                        principalTable: "SpesifikasiJenis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LatihanTrx_LatihanId",
                table: "LatihanTrx",
                column: "LatihanId");

            migrationBuilder.CreateIndex(
                name: "IX_LLPTrx_SpesifikasiJenisId",
                table: "LLPTrx",
                column: "SpesifikasiJenisId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonilTrx_PersonilId",
                table: "PersonilTrx",
                column: "PersonilId");

            migrationBuilder.CreateIndex(
                name: "IX_SpesifikasiJenis_PeralatanOSRId",
                table: "SpesifikasiJenis",
                column: "PeralatanOSRId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LatihanTrx");

            migrationBuilder.DropTable(
                name: "LLPTrx");

            migrationBuilder.DropTable(
                name: "PersonilTrx");

            migrationBuilder.DropTable(
                name: "Latihan");

            migrationBuilder.DropTable(
                name: "SpesifikasiJenis");

            migrationBuilder.DropTable(
                name: "Personil");

            migrationBuilder.DropTable(
                name: "PeralatanOSR");
        }
    }
}
