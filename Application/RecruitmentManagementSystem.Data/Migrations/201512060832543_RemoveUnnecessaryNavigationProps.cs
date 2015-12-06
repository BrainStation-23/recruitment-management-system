namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnnecessaryNavigationProps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Experiences", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Projects", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Skills", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Educations", new[] { "CandidateId" });
            DropIndex("dbo.Experiences", new[] { "CandidateId" });
            DropIndex("dbo.Projects", new[] { "CandidateId" });
            DropIndex("dbo.Skills", new[] { "CandidateId" });
            RenameColumn(table: "dbo.Educations", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Experiences", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Files", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Projects", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Skills", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Files", name: "QuestionId", newName: "Question_Id");
            RenameIndex(table: "dbo.Files", name: "IX_CandidateId", newName: "IX_Candidate_Id");
            RenameIndex(table: "dbo.Files", name: "IX_QuestionId", newName: "IX_Question_Id");
            AlterColumn("dbo.Educations", "Candidate_Id", c => c.Int());
            AlterColumn("dbo.Experiences", "Candidate_Id", c => c.Int());
            AlterColumn("dbo.Projects", "Candidate_Id", c => c.Int());
            AlterColumn("dbo.Skills", "Candidate_Id", c => c.Int());
            CreateIndex("dbo.Educations", "Candidate_Id");
            CreateIndex("dbo.Experiences", "Candidate_Id");
            CreateIndex("dbo.Projects", "Candidate_Id");
            CreateIndex("dbo.Skills", "Candidate_Id");
            AddForeignKey("dbo.Educations", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Experiences", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Projects", "Candidate_Id", "dbo.Candidates", "Id");
            AddForeignKey("dbo.Skills", "Candidate_Id", "dbo.Candidates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Projects", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Experiences", "Candidate_Id", "dbo.Candidates");
            DropForeignKey("dbo.Educations", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Skills", new[] { "Candidate_Id" });
            DropIndex("dbo.Projects", new[] { "Candidate_Id" });
            DropIndex("dbo.Experiences", new[] { "Candidate_Id" });
            DropIndex("dbo.Educations", new[] { "Candidate_Id" });
            AlterColumn("dbo.Skills", "Candidate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "Candidate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Experiences", "Candidate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Educations", "Candidate_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Files", name: "IX_Question_Id", newName: "IX_QuestionId");
            RenameIndex(table: "dbo.Files", name: "IX_Candidate_Id", newName: "IX_CandidateId");
            RenameColumn(table: "dbo.Files", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Skills", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Projects", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Files", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Experiences", name: "Candidate_Id", newName: "CandidateId");
            RenameColumn(table: "dbo.Educations", name: "Candidate_Id", newName: "CandidateId");
            CreateIndex("dbo.Skills", "CandidateId");
            CreateIndex("dbo.Projects", "CandidateId");
            CreateIndex("dbo.Experiences", "CandidateId");
            CreateIndex("dbo.Educations", "CandidateId");
            AddForeignKey("dbo.Skills", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Experiences", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Educations", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
        }
    }
}
