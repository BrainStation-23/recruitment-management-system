using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public bool StillWorking { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}