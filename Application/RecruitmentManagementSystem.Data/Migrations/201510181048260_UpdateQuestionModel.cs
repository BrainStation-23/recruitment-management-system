namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuestionModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            AddColumn("dbo.Questions", "Text", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Questions", "QuestionType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Answer", c => c.String(maxLength: 300));
            AddColumn("dbo.Choices", "IsValid", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "Title");
            DropColumn("dbo.Questions", "Type");
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
            
            AddColumn("dbo.Questions", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Choices", "IsValid");
            DropColumn("dbo.Questions", "Answer");
            DropColumn("dbo.Questions", "QuestionType");
            DropColumn("dbo.Questions", "Text");
            CreateIndex("dbo.Answers", "Question_Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
        }
    }
}
