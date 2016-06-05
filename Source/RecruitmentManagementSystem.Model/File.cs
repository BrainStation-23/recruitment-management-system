using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public enum FileType
    {
        Avatar = 1,
        Resume = 2,
        Document = 3
    }

    public class File : BaseEntity
    {
        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string RelativePath { get; set; }

        [Required]
        public FileType FileType { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }

        public Question Question { get; set; }

        public User User { get; set; }
    }
}
