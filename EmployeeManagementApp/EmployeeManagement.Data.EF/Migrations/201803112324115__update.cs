namespace EmployeeManagement.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropPrimaryKey("dbo.Department");
            AlterColumn("dbo.Department", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Department", "ID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropPrimaryKey("dbo.Department");
            AlterColumn("dbo.Department", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Department", "ID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "ID", cascadeDelete: true);
        }
    }
}
