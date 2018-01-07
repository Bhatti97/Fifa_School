namespace Fifa_school.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Branch_id = c.Int(nullable: false, identity: true),
                        Branch_name = c.String(),
                        Branch_Address = c.String(),
                        Branch_Contact = c.String(),
                    })
                .PrimaryKey(t => t.Branch_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Branches");
        }
    }
}
