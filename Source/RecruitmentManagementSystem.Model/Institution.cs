using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Institution : BaseEntity
    {
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string City { get; set; }
    }
}
