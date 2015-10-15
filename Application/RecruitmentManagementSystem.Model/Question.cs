using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum QuestionType
    {
        Mcq = 1,
        Descriptive = 2
    }

    public class Question : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
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
    }
}