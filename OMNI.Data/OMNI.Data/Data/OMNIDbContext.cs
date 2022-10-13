using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMNI.Data.Data.Dao;
using OMNI.Migrations.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Data.Data
{
    public class OMNIDbContext : DbContext
    {
        public OMNIDbContext(DbContextOptions<OMNIDbContext> options) : base(options)
        {

        }
        public DbSet<Kondisi> Kondisi { get; set; }
        public DbSet<HistoryLLPTrx> HistoryLLPTrx { get; set; }
        public DbSet<LLPTrx> LLPTrx { get; set; }
        public DbSet<PeralatanOSR> PeralatanOSR { get; set; }
        public DbSet<Jenis> Jenis { get; set; }
        public DbSet<SpesifikasiJenis> SpesifikasiJenis { get; set; }
        public DbSet<RekomendasiJenis> RekomendasiJenis { get; set; }
        public DbSet<Latihan> Latihan { get; set; }
        public DbSet<LatihanTrx> LatihanTrx { get; set; }
        public DbSet<HistoryLatihanTrx> HistoryLatihanTrx { get; set; }
        public DbSet<RekomendasiLatihan> RekomendasiLatihan { get; set; }
        public DbSet<RekomendasiType> RekomendasiType { get; set; }
        public DbSet<Personil> Personil { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PersonilTrx> PersonilTrx { get; set; }
        public DbSet<HistoryPersonilTrx> HistoryPersonilTrx { get; set; }
        public DbSet<RekomendasiPersonil> RekomendasiPersonil { get; set; }
        public DbSet<FileUpload> FileUpload { get; set; }
        public DbSet<Lampiran> Lampiran { get; set; }
        public DbSet<GuestLocation> GuestLocation { get; set; }
        public DbSet<LLPHistoryStatus> LLPHistoryStatus { get; set; }
        public DbSet<OMNIActivityLog> OMNIActivityLog { get; set; }
    }
}
