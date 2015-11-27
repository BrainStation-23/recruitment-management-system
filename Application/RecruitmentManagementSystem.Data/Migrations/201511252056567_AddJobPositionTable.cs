namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPositionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Candidates", "JobPositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Candidates", "JobPositionId");
            AddForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions");
            DropIndex("dbo.Candidates", new[] { "JobPositionId" });
            DropColumn("dbo.Candidates", "JobPositionId");
            DropTable("dbo.JobPositions");
        }
    }
}
