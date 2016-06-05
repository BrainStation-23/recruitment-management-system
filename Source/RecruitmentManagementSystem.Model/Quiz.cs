using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Quiz : BaseEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<QuizPage> QuizPages { get; set; }
    }
}
