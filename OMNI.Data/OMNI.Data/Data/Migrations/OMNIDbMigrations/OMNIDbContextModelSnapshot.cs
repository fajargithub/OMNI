﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMNI.Data.Data;

namespace OMNI.Data.Data.Migrations.OMNIDbMigrations
{
    [DbContext(typeof(OMNIDbContext))]
    partial class OMNIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryLLPTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("DetailExisting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianMP58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KesesuaianOSCP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kondisi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PersentaseOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SelisihHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SelisihOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SpesifikasiJenisId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalExistingJenis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalExistingKeseluruhan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalKebutuhanHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalKebutuhanOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("SpesifikasiJenisId");

                    b.ToTable("HistoryLLPTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryLatihanTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianPM58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LatihanId")
                        .HasColumnType("int");

                    b.Property<float>("PersentaseLatihan")
                        .HasColumnType("real");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SelisihHubla")
                        .HasColumnType("real");

                    b.Property<DateTime>("TanggalPelaksanaan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("LatihanId");

                    b.ToTable("HistoryLatihanTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryPersonilTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianPM58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PersentasePersonil")
                        .HasColumnType("real");

                    b.Property<int?>("PersonilId")
                        .HasColumnType("int");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SelisihHubla")
                        .HasColumnType("real");

                    b.Property<int>("SisaMasaBerlaku")
                        .HasColumnType("int");

                    b.Property<DateTime>("TanggalExpired")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TanggalPelatihan")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalDetailExisting")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("PersonilId");

                    b.ToTable("HistoryPersonilTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.Jenis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Satuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Jenis");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.Kondisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Kondisi");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.LLPTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("DetailExisting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianMP58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KesesuaianOSCP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kondisi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PersentaseHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PersentaseOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SelisihHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SelisihOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SpesifikasiJenisId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalExistingJenis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalExistingKeseluruhan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalKebutuhanHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalKebutuhanOSCP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("SpesifikasiJenisId");

                    b.ToTable("LLPTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.Latihan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Satuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Latihan");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.LatihanTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianPM58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LatihanId")
                        .HasColumnType("int");

                    b.Property<float>("PersentaseLatihan")
                        .HasColumnType("real");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SelisihHubla")
                        .HasColumnType("real");

                    b.Property<DateTime>("TanggalPelaksanaan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("LatihanId");

                    b.ToTable("LatihanTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.OMNIActivityLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Controller")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("TrxId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("OMNIActivityLog");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.PeralatanOSR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("PeralatanOSR");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.Personil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Satuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Personil");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.PersonilTrx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("KesesuaianPM58")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PersentasePersonil")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PersonilId")
                        .HasColumnType("int");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SelisihHubla")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SisaMasaBerlaku")
                        .HasColumnType("int");

                    b.Property<DateTime>("TanggalExpired")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TanggalPelatihan")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalDetailExisting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("PersonilId");

                    b.ToTable("PersonilTrx");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiJenis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RekomendasiTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("SpesifikasiJenisId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("RekomendasiTypeId");

                    b.HasIndex("SpesifikasiJenisId");

                    b.ToTable("RekomendasiJenis");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiLatihan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<int?>("LatihanId")
                        .HasColumnType("int");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RekomendasiTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("LatihanId");

                    b.HasIndex("RekomendasiTypeId");

                    b.ToTable("RekomendasiLatihan");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiPersonil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<int?>("PersonilId")
                        .HasColumnType("int");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RekomendasiTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PersonilId");

                    b.HasIndex("RekomendasiTypeId");

                    b.ToTable("RekomendasiPersonil");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("RekomendasiType");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.SpesifikasiJenis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("IsDeleted")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<int?>("JenisId")
                        .HasColumnType("int");

                    b.Property<int?>("PeralatanOSRId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("JenisId");

                    b.HasIndex("PeralatanOSRId");

                    b.ToTable("SpesifikasiJenis");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryLLPTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.SpesifikasiJenis", "SpesifikasiJenis")
                        .WithMany()
                        .HasForeignKey("SpesifikasiJenisId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryLatihanTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Latihan", "Latihan")
                        .WithMany()
                        .HasForeignKey("LatihanId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.HistoryPersonilTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Personil", "Personil")
                        .WithMany()
                        .HasForeignKey("PersonilId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.LLPTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.SpesifikasiJenis", "SpesifikasiJenis")
                        .WithMany()
                        .HasForeignKey("SpesifikasiJenisId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.LatihanTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Latihan", "Latihan")
                        .WithMany()
                        .HasForeignKey("LatihanId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.PersonilTrx", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Personil", "Personil")
                        .WithMany()
                        .HasForeignKey("PersonilId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiJenis", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.RekomendasiType", "RekomendasiType")
                        .WithMany()
                        .HasForeignKey("RekomendasiTypeId");

                    b.HasOne("OMNI.Data.Data.Dao.SpesifikasiJenis", "SpesifikasiJenis")
                        .WithMany()
                        .HasForeignKey("SpesifikasiJenisId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiLatihan", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Latihan", "Latihan")
                        .WithMany()
                        .HasForeignKey("LatihanId");

                    b.HasOne("OMNI.Data.Data.Dao.RekomendasiType", "RekomendasiType")
                        .WithMany()
                        .HasForeignKey("RekomendasiTypeId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.RekomendasiPersonil", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Personil", "Personil")
                        .WithMany()
                        .HasForeignKey("PersonilId");

                    b.HasOne("OMNI.Data.Data.Dao.RekomendasiType", "RekomendasiType")
                        .WithMany()
                        .HasForeignKey("RekomendasiTypeId");
                });

            modelBuilder.Entity("OMNI.Data.Data.Dao.SpesifikasiJenis", b =>
                {
                    b.HasOne("OMNI.Data.Data.Dao.Jenis", "Jenis")
                        .WithMany()
                        .HasForeignKey("JenisId");

                    b.HasOne("OMNI.Data.Data.Dao.PeralatanOSR", "PeralatanOSR")
                        .WithMany()
                        .HasForeignKey("PeralatanOSRId");
                });
#pragma warning restore 612, 618
        }
    }
}
