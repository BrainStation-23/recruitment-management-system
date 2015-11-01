using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum QuestionType
    {
        MCQ = 1,
        Descriptive = 2
    }

    public class Question : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Answer { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual QuestionCategory Category { get; set; }
    }

    public class Choice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        public bool IsValid { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}