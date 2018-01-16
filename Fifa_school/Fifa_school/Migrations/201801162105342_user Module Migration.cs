namespace Fifa_school.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userModuleMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        user_role = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
