namespace EmployeeManagement.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _UpdateModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MidleName = c.String(),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Profession = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "DepartmentID" });
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
        }
    }
}
