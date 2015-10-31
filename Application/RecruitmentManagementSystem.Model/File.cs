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
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string RelativePath { get; set; }

        [Required]
        public FileType FileType { get; set; }

        public string MimeType { get; set; }

        public int Size { get; set; }

        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int? CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}