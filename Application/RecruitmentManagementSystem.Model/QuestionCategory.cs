using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class QuestionCategory : BaseEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}