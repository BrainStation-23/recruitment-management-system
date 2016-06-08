using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum QuestionType
    {
        Descriptive = 1,

        [Display(Name = "Multiple choice question")]
        MultipleChoiseQuestion = 2,

        [Display(Name = "Single choice question")]
        SingleChoiceQuestion = 3
    }

    public class Question : BaseEntity
    {
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        public ICollection<File> Files { get; set; }

        public ICollection<Choice> Choices { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Answer { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public decimal DefaultPoint { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public QuestionCategory Category { get; set; }
    }

    public class Choice : BaseEntity
    {
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [DefaultValue(false)]
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
