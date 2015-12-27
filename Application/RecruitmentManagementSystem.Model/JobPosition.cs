using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class JobPosition : BaseEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}