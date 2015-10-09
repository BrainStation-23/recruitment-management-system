using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum QuestionType
    {
        Mcq = 1,
        Descriptive = 2
    }

    public enum DisplayType
    {
        Radio = 1,
        ChackBox = 2,
        TextArea = 3
    }

    public class Question : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Tittle { get; set; }

        public QuestionType Type { get; set; }
        public DisplayType DisplayType { get; set; }

        public IList<File> Images { get; set; }
        public IList<Answer> Answers { get; set; }
        public IList<Choice> Choices { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Note { get; set; }

        public int CategoryId { get; set; }
        public virtual QuestionCategory Category { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }
    }

    public class Choice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}