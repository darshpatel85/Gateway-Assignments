namespace SourceControlAss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 10),
                        DOB = c.String(nullable: false),
                        MNO = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
