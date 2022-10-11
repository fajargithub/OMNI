using Microsoft.EntityFrameworkCore;
using OMNI.Data.Data.Dao.CorePTK;
using OMNI.Migrations.Data.Dao.CorePTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Data.Data
{
    public class CorePTKContext : DbContext
    {
        public CorePTKContext(DbContextOptions<CorePTKContext> options) : base(options)
        {

        }

        public DbSet<Port> Port { get; set; }
        public DbSet<PArea> PAreas { get; set; }
        public DbSet<PAreaSub> PAreaSubs { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<BranchOld> BranchOld { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<EmployeePositionAbbrv> EmployeePositionAbbrvs { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PortEmployee> PortEmployees { get; set; }
        public DbSet<Religion> Religion { get; set; }
        public DbSet<Ship> Ship { get; set; }
        public DbSet<ShipAuthor> ShipAuthor { get; set; }
        public DbSet<ShipCategory> ShipCategory { get; set; }
        public DbSet<ShipEmployee> ShipEmployees { get; set; }
        public DbSet<ShipOwner> ShipOwner { get; set; }
        public DbSet<ShipStatus> ShipStatus { get; set; }

    }
}
