using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tittle")]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Tittle { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Question Type")]
        public QuestionType Type { get; set; }

        [Display(Name = "Answer Display Type")]
        public DisplayType DisplayType { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Note { get; set; }
    }
}