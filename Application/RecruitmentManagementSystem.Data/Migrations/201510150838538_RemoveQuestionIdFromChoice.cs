namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQuestionIdFromChoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            RenameColumn(table: "dbo.Choices", name: "QuestionId", newName: "Question_Id");
            AlterColumn("dbo.Choices", "Question_Id", c => c.Int());
            CreateIndex("dbo.Choices", "Question_Id");
            AddForeignKey("dbo.Choices", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choices", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Choices", new[] { "Question_Id" });
            AlterColumn("dbo.Choices", "Question_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Choices", name: "Question_Id", newName: "QuestionId");
            CreateIndex("dbo.Choices", "QuestionId");
            AddForeignKey("dbo.Choices", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
