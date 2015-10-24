namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorEntityModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "Avatar_Id", "dbo.Files");
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Educations", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Experiences", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Projects", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Choices", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Candidates", new[] { "Avatar_Id" });
            DropIndex("dbo.Educations", new[] { "Candidate_Id" });
            DropIndex("dbo.Experiences", new[] { "Candidate_Id" });
            DropIndex("dbo.Projects", new[] { "Candidate_Id" });
            DropIndex("dbo.Skills", new[] { "Candidate_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Choices", new[] { "Question_Id" });
            RenameColumn(table: "dbo.Educations", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Experiences", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Projects", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Choices", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Files", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Avatar_Id", newName: "AvatarId");
            RenameColumn(table: "dbo.Files", name: "Candidate_Id", newName: "CandidateId");
            RenameIndex(table: "dbo.Files", name: "IX_Question_Id", newName: "IX_QuestionId");
            RenameIndex(table: "dbo.Files", name: "IX_Candidate_Id", newName: "IX_CandidateId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Avatar_Id", newName: "IX_AvatarId");
            CreateTable(
                "dbo.SkillCandidates",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Candidate_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Candidate_Id);
            
            AddColumn("dbo.Files", "RelativePath", c => c.String(nullable: false));
            AddColumn("dbo.Files", "MimeType", c => c.String());
            AddColumn("dbo.Files", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Institutions", "City", c => c.String());
            AddColumn("dbo.Questions", "Text", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Questions", "QuestionType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Answer", c => c.String(maxLength: 300));
            AddColumn("dbo.Choices", "IsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CandidateId", c => c.Int());
            AlterColumn("dbo.Files", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Files", "FileType", c => c.Int(nullable: false));
            AlterColumn("dbo.Educations", "CandidateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Experiences", "CandidateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "CandidateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Choices", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Candidates", "Email", unique: true, name: "EmailIndex");
            CreateIndex("dbo.Educations", "CandidateId");
            CreateIndex("dbo.Experiences", "CandidateId");
            CreateIndex("dbo.Choices", "QuestionId");
            CreateIndex("dbo.Projects", "CandidateId");
            CreateIndex("dbo.AspNetUsers", "CandidateId");
            AddForeignKey("dbo.AspNetUsers", "CandidateId", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Educations", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Experiences", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Choices", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            DropColumn("dbo.Candidates", "Avatar_Id");
            DropColumn("dbo.Files", "Path");
            DropColumn("dbo.Skills", "Candidate_Id");
            DropColumn("dbo.Questions", "Title");
            DropColumn("dbo.Questions", "Type");
            DropColumn("dbo.AspNetUsers", "CanidateId");
            DropTable("dbo.Answers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 100),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "CanidateId", c => c.Int());
            AddColumn("dbo.Questions", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Skills", "Candidate_Id", c => c.Int());
            AddColumn("dbo.Files", "Path", c => c.String(nullable: false));
            AddColumn("dbo.Candidates", "Avatar_Id", c => c.Int());
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Projects", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Experiences", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Educations", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.AspNetUsers", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.SkillCandidates", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.SkillCandidates", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillCandidates", new[] { "Candidate_Id" });
            DropIndex("dbo.SkillCandidates", new[] { "Skill_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CandidateId" });
            DropIndex("dbo.Projects", new[] { "CandidateId" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropIndex("dbo.Experiences", new[] { "CandidateId" });
            DropIndex("dbo.Educations", new[] { "CandidateId" });
            DropIndex("dbo.Candidates", "EmailIndex");
            AlterColumn("dbo.Choices", "QuestionId", c => c.Int());
            AlterColumn("dbo.Projects", "CandidateId", c => c.Int());
            AlterColumn("dbo.Experiences", "CandidateId", c => c.Int());
            AlterColumn("dbo.Educations", "CandidateId", c => c.Int());
            AlterColumn("dbo.Files", "FileType", c => c.String());
            AlterColumn("dbo.Files", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.AspNetUsers", "CandidateId");
            DropColumn("dbo.Choices", "IsValid");
            DropColumn("dbo.Questions", "Answer");
            DropColumn("dbo.Questions", "QuestionType");
            DropColumn("dbo.Questions", "Text");
            DropColumn("dbo.Institutions", "City");
            DropColumn("dbo.Files", "Size");
            DropColumn("dbo.Files", "MimeType");
            DropColumn("dbo.Files", "RelativePath");
            DropTable("dbo.SkillCandidates");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AvatarId", newName: "IX_Avatar_Id");
            RenameIndex(table: "dbo.Files", name: "IX_CandidateId", newName: "IX_Candidate_Id");
            RenameIndex(table: "dbo.Files", name: "IX_QuestionId", newName: "IX_Question_Id");
            RenameColumn(table: "dbo.Files", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "AvatarId", newName: "Avatar_Id");
            RenameColumn(table: "dbo.Files", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.Choices", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.Projects", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Experiences", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Educations", name: "CandidateId", newName: "Candidate_Id");
            CreateIndex("dbo.Choices", "Question_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Skills", "Candidate_Id");
            CreateIndex("dbo.Projects", "Candidate_Id");
            CreateIndex("dbo.Experiences", "Candidate_Id");
            CreateIndex("dbo.Educations", "Candidate_Id");
            CreateIndex("dbo.Candidates", "Avatar_Id");
            AddForeignKey("dbo.Choices", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Projects", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Experiences", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Educations", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Skills", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Candidates", "Avatar_Id", "dbo.Files", "Id");
        }
    }
}
