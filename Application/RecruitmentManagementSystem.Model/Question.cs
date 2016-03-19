using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum QuestionType
    {
        Descriptive = 1,
        MultipleChoiseQuestion = 2,
        SingleChoiceQuestion = 3
    }

    public class Question : BaseEntity
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        public ICollection<File> Files { get; set; }

        public ICollection<Answer> Answers { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public decimal DefaultPoint { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public QuestionCategory Category { get; set; }
    }

    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }

        [DefaultValue(false)]
        public bool IsCorrect { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string AnswerText { get; set; }

        public Question Question { get; set; }
    }
}