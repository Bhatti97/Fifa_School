namespace Fifa_school.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duetoaddTeacherEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Teacher_id = c.Int(nullable: false, identity: true),
                        Teacher_Name = c.String(),
                        Teacher_FatherName = c.String(),
                        Dateofjoin = c.DateTime(nullable: false),
                        Branch_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Teacher_id)
                .ForeignKey("dbo.Branches", t => t.Branch_id, cascadeDelete: true)
                .Index(t => t.Branch_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Branch_id", "dbo.Branches");
            DropIndex("dbo.Teachers", new[] { "Branch_id" });
            DropTable("dbo.Teachers");
        }
    }
}
