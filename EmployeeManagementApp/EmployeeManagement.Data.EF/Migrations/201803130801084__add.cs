namespace EmployeeManagement.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _add : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "TeamLead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "TeamLead", c => c.Int(nullable: false));
        }
    }
}
