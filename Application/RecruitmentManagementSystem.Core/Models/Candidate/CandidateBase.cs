using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Candidate
{
    public class CandidateBase : BaseDto
    {
        public CandidateBase()
        {
            Educations = new List<EducationDto>();
            Experiences = new List<ExperienceDto>();
            Projects = new List<ProjectDto>();
            Skills = new List<SkillDto>();
        }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail Address")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Others")]
        [DataType(DataType.MultilineText)]
        [StringLength(3000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Others { get; set; }

        [Url]
        [Display(Name = "Website")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Job Position")]
        public int JobPositionId { get; set; }

        public ICollection<EducationDto> Educations { get; set; }

        public ICollection<ExperienceDto> Experiences { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }

        public ICollection<SkillDto> Skills { get; set; }

        public string AvatarFileName { get; set; }

        public string ResumeFileName { get; set; }

        public ICollection<string> DocumentFileNames { get; set; }
    }
}