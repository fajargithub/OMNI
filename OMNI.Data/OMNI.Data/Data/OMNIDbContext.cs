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
    public class OMNIDbContext : IdentityDbContext<IdentityUser>
    {
        public OMNIDbContext(DbContextOptions<OMNIDbContext> options) : base(options)
        {

        }

        public DbSet<PeralatanOSR> PeralatanOSR { get; set; }
    }
}
