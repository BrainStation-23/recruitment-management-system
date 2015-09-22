using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class BaseCategory
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}