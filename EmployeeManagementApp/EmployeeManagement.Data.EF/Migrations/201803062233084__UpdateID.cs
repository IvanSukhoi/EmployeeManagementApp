namespace EmployeeManagement.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _UpdateID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employee");
            DropColumn("dbo.Employee", "EmployeeID");
            AddColumn("dbo.Employee", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employee", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "EmployeeID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Employee");
            DropColumn("dbo.Employee", "ID");
            AddPrimaryKey("dbo.Employee", "EmployeeID");
        }
    }
}
