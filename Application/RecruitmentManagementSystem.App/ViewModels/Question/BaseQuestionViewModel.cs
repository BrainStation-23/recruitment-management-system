using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class BaseQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Choices")]
        public ICollection<string> Choices { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}