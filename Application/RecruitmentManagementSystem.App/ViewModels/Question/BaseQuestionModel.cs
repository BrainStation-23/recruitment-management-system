using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class BaseQuestionModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Text")]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Choices")]
        public ICollection<Choice> Choices { get; set; }

        public ICollection<File> Files { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}