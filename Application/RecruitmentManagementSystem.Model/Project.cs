using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Project : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Url]
        public string Url { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}