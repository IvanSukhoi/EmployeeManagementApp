using EmployeeManagement.Data.EF.Entities;
using System.Data.Entity;

namespace EmployeeManagement.Data.EF.DAL
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext():base("EmployeeManagementApp")
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employee");
                //.HasRequired(x => x.Department)
                //.WithRequiredPrincipal(x => x.Leader);

            modelBuilder.Entity<DepartmentEntity>().ToTable("Department");
                //HasMany(x => x.Employees)
                //.HasForeignKey(x => x.DepartmentID);

           // base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
