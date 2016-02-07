namespace RecruitmentManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseAndQuizEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions");
            DropIndex("dbo.Candidates", new[] { "JobPositionId" });
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 300),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        CourseId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.QuizPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayOrder = c.Int(nullable: false),
                        QuizId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Point = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(nullable: false),
                        UpdatedBy = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        QuizPage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .ForeignKey("dbo.QuizPages", t => t.QuizPage_Id)
                .Index(t => t.QuizId)
                .Index(t => t.QuestionId)
                .Index(t => t.QuizPage_Id);
            
            AddColumn("dbo.Questions", "DefaultPoint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Candidates", "JobPositionId");
            DropColumn("dbo.Choices", "IsValid");
            DropTable("dbo.JobPositions");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Choices", "IsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Candidates", "JobPositionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuizQuestions", "QuizPage_Id", "dbo.QuizPages");
            DropForeignKey("dbo.QuizQuestions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.QuizQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuizPages", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "CourseId", "dbo.Courses");
            DropIndex("dbo.QuizQuestions", new[] { "QuizPage_Id" });
            DropIndex("dbo.QuizQuestions", new[] { "QuestionId" });
            DropIndex("dbo.QuizQuestions", new[] { "QuizId" });
            DropIndex("dbo.QuizPages", new[] { "QuizId" });
            DropIndex("dbo.Quizs", new[] { "CourseId" });
            DropColumn("dbo.Questions", "DefaultPoint");
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuizPages");
            DropTable("dbo.Quizs");
            DropTable("dbo.Courses");
            CreateIndex("dbo.Candidates", "JobPositionId");
            AddForeignKey("dbo.Candidates", "JobPositionId", "dbo.JobPositions", "Id", cascadeDelete: true);
        }
    }
}
