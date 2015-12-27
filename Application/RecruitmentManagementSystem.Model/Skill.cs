using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Skill : BaseEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}