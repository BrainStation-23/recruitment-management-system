namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredInProjectDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "Activities", c => c.String(maxLength: 1500));
            AlterColumn("dbo.Projects", "Description", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.Educations", "Activites");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Educations", "Activites", c => c.String(maxLength: 1500));
            AlterColumn("dbo.Projects", "Description", c => c.String(maxLength: 1000));
            DropColumn("dbo.Educations", "Activities");
        }
    }
}
