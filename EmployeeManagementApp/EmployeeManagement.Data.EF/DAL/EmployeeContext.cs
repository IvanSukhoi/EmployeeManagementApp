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
            modelBuilder.Entity<DepartmentEntity>().ToTable("Department");
        }
    }
}
