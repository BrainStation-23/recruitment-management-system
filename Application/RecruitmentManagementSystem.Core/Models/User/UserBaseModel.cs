using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class UserBaseModel
    {
        public UserBaseModel()
        {
            Educations = new List<EducationModel>();
            Experiences = new List<ExperienceModel>();
            Projects = new List<ProjectModel>();
            Skills = new List<SkillModel>();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string LastName { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Url]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Website { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Address { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(3000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Others { get; set; }

        public ICollection<EducationModel> Educations { get; set; }

        public ICollection<ExperienceModel> Experiences { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }

        public ICollection<SkillModel> Skills { get; set; }

        public ICollection<FileModel> Files { get; set; }
    }
}