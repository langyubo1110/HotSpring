using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class HotSpringDbContext:DbContext
    {
        public HotSpringDbContext() : base("HS")
        {

        }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EmployEmp> EmployEmps { get; set; }
        public DbSet<SystemLogs> SystemLogs { get; set; }
        public DbSet<SystemModule> SystemModules { get; set; }
        public DbSet<RepoBuy> RepoBuy { get; set; }
        public DbSet<RepoGoodsStock> RepoGoodsStock { get; set; }
        public DbSet<RepoOutInRecord> RepoOutInRecord { get; set; }
        public DbSet<RegAudit> regAudit { get; set; }
        public DbSet<EquipType> EquipType { get; set; }
        public DbSet<RegApply> RegApply { get; set; }
        public DbSet<RegEquipRes> RegEquipRes { get; set; }
        public DbSet<SystemPageCorrespondence> systemPageCorrespondences { get; set; }
        public DbSet<SystemPages> systemPages { get; set; }
        public DbSet<EmployRole> EmployRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
