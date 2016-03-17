using System;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Institution;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Core.Models.User;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Models.Candidate
{
    public class EducationModel : BaseModel, IMapFrom<Education>
    {
        [Required]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "Field of Study")]
        public string FieldOfStudy { get; set; }

        public double Grade { get; set; }

        [StringLength(1500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Activities { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Currently Present")]
        public bool Present { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public int InstitutionId { get; set; }

        public InstitutionModel Institution { get; set; }
    }
}