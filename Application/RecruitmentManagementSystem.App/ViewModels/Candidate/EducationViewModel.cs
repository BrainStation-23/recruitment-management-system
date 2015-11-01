using System;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class EducationViewModel : IMapFrom<Model.Education>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "Field of Study")]
        public string FieldOfStudy { get; set; }

        public double Grade { get; set; }

        [StringLength(1500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Activites { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Currently Present")]
        public bool CurrentlyPresent { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public int InstitutionId { get; set; }
    }
}