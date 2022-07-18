using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMNI.Data.Data.Dao;
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

        public DbSet<PeralatanOSR> PeralatanOSR { get; set; }
        public DbSet<SpesifikasiJenis> SpesifikasiJenis { get; set; }
        public DbSet<Latihan> Latihan { get; set; }
        public DbSet<LatihanTrx> LatihanTrx { get; set; }
        public DbSet<Personil> Personil { get; set; }
        public DbSet<PersonilTrx> PersonilTrx { get; set; }
        public DbSet<LLPTrx> LLPTrx { get; set; }
        public DbSet<OMNIActivityLog> OMNIActivityLog { get; set; }
    }
}
