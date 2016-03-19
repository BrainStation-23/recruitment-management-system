using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Course : BaseEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public ICollection<Quiz> Quizzes { get; set; }
    }
}