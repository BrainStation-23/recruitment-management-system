using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class File : BaseEntity
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public string FileType { get; set; }
    }
}