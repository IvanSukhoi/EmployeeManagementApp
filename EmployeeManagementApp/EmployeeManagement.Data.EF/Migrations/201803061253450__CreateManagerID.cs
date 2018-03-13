namespace EmployeeManagement.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _CreateManagerID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "ManagerID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ManagerID");
        }
    }
}
