using System;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Candidate
{
    public class ExperienceModel : BaseModel, IMapFrom<Model.Experience>
    {
        [Required]
        [Display(Name = "Company Name")]
        public string Organization { get; set; }

        [Display(Name = "Job Tittle")]
        public string JobTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool StillWorking { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        public int CandidateId { get; set; }
    }
}