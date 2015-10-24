using System;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Experience
    {
        public int Id { get; set; }

        [Required]
        public string Organization { get; set; }

        public string JobTitle { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public bool StillWorking { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}