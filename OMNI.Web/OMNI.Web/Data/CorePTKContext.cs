using Microsoft.EntityFrameworkCore;
using OMNI.Web.Data.Dao.CorePTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Data.Dao
{
    public class CorePTKContext : DbContext
    {
        public CorePTKContext(DbContextOptions<CorePTKContext> options) : base(options)
        {

        }
        public DbSet<Port> Port { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<PAreaSub> PAreaSub { get; set; }
        public DbSet<PArea> PArea { get; set; }
        public DbSet<BranchOld> BranchOld { get; set; }
    }
}
