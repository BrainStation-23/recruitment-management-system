namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFileEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Files");
            DropIndex("dbo.AspNetUsers", new[] { "AvatarId" });
            AddColumn("dbo.Files", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Files", "ApplicationUserId");
            AddForeignKey("dbo.Files", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "AvatarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AvatarId", c => c.Int());
            DropForeignKey("dbo.Files", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "ApplicationUserId" });
            DropColumn("dbo.Files", "ApplicationUserId");
            CreateIndex("dbo.AspNetUsers", "AvatarId");
            AddForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Files", "Id");
        }
    }
}
