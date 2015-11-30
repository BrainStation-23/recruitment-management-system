using System;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class ExperienceViewModel : IMapFrom<Model.Experience>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Organization { get; set; }

        [Display(Name = "Job Tittle")]
        public string JobTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Present { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }
    }
}