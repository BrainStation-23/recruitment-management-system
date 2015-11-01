using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class Experience
    {
        public int Id { get; set; }

        [Required]
        public string Organization { get; set; }

        public string JobTitle { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? EndDate { get; set; }

        public bool StillWorking { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}