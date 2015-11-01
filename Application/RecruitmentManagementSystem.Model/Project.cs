using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Url]
        public string Url { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}