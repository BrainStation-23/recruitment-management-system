using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string FieldOfStudy { get; set; }

        public double Grade { get; set; }

        [StringLength(1500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Activites { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public int FirstYear { get; set; }

        public int LastYear { get; set; }

        public int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}