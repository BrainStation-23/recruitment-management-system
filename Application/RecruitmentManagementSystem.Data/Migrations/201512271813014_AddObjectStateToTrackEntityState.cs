namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddObjectStateToTrackEntityState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Files");
            DropIndex("dbo.AspNetUsers", new[] { "AvatarId" });
            RenameColumn(table: "dbo.Files", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Files", name: "QuestionId", newName: "Question_Id");
            RenameIndex(table: "dbo.Files", name: "IX_CandidateId", newName: "IX_Candidate_Id");
            RenameIndex(table: "dbo.Files", name: "IX_QuestionId", newName: "IX_Question_Id");
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Candidates", "JobPositionId", c => c.Int(nullable: false));
            AddColumn("dbo.Educations", "Activities", c => c.String(maxLength: 1500));
            AddColumn("dbo.Educations", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Educations", "UpdatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Educations", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Educations", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Experiences", "StillWorking", c => c.Boolean(nullable: false));
            AddColumn("dbo.Experiences", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Experiences", "UpdatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Experiences", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Experiences", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Files", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Choices", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Choices", "UpdatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Choices", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Choices", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Projects", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "UpdatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Projects", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Candidates", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Candidates", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Candidates", "UpdatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Institutions", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Institutions", "UpdatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "UpdatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "UpdatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.QuestionCategories", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.QuestionCategories", "UpdatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Description", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Skills", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Skills", "UpdatedBy", c => c.String(nullable: false));
            CreateIndex("dbo.Candidates", "PhoneNumber", unique: true, name: "PhoneNumberIndex");
            CreateIndex("dbo.Candidates", "JobPositionId");
            CreateIndex("dbo.Files", "ApplicationUserId");
            AddForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Files", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Educations", "Activites");
            DropColumn("dbo.Experiences", "Present");
            DropColumn("dbo.AspNetUsers", "AvatarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AvatarId", c => c.Int());
            AddColumn("dbo.Experiences", "Present", c => c.Boolean(nullable: false));
            AddColumn("dbo.Educations", "Activites", c => c.String(maxLength: 1500));
            DropForeignKey("dbo.Files", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions");
            DropIndex("dbo.Files", new[] { "ApplicationUserId" });
            DropIndex("dbo.Candidates", new[] { "JobPositionId" });
            DropIndex("dbo.Candidates", "PhoneNumberIndex");
            AlterColumn("dbo.Skills", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Skills", "CreatedBy", c => c.String());
            AlterColumn("dbo.Projects", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.QuestionCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.QuestionCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.Questions", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Questions", "CreatedBy", c => c.String());
            AlterColumn("dbo.Files", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Files", "CreatedBy", c => c.String());
            AlterColumn("dbo.Institutions", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Institutions", "CreatedBy", c => c.String());
            AlterColumn("dbo.Candidates", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Candidates", "CreatedBy", c => c.String());
            AlterColumn("dbo.Candidates", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.Projects", "UpdatedAt");
            DropColumn("dbo.Projects", "CreatedAt");
            DropColumn("dbo.Projects", "UpdatedBy");
            DropColumn("dbo.Projects", "CreatedBy");
            DropColumn("dbo.Choices", "UpdatedAt");
            DropColumn("dbo.Choices", "CreatedAt");
            DropColumn("dbo.Choices", "UpdatedBy");
            DropColumn("dbo.Choices", "CreatedBy");
            DropColumn("dbo.Files", "ApplicationUserId");
            DropColumn("dbo.Experiences", "UpdatedAt");
            DropColumn("dbo.Experiences", "CreatedAt");
            DropColumn("dbo.Experiences", "UpdatedBy");
            DropColumn("dbo.Experiences", "CreatedBy");
            DropColumn("dbo.Experiences", "StillWorking");
            DropColumn("dbo.Educations", "UpdatedAt");
            DropColumn("dbo.Educations", "CreatedAt");
            DropColumn("dbo.Educations", "UpdatedBy");
            DropColumn("dbo.Educations", "CreatedBy");
            DropColumn("dbo.Educations", "Activities");
            DropColumn("dbo.Candidates", "JobPositionId");
            DropTable("dbo.JobPositions");
            RenameIndex(table: "dbo.Files", name: "IX_Question_Id", newName: "IX_QuestionId");
            RenameIndex(table: "dbo.Files", name: "IX_Candidate_Id", newName: "IX_CandidateId");
            RenameColumn(table: "dbo.Files", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Files", name: "Candidate_Id", newName: "CandidateId");
            CreateIndex("dbo.AspNetUsers", "AvatarId");
            AddForeignKey("dbo.AspNetUsers", "AvatarId", "dbo.Files", "Id");
        }
    }
}
