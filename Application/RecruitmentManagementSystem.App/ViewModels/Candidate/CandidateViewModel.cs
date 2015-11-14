using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class CandidateViewModel : IMapFrom<Model.Candidate>
    {
        public int Id { get; set; }

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

        public ICollection<EducationViewModel> Educations { get; set; }

        public ICollection<ExperienceViewModel> Experiences { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

        public ICollection<SkillViewModel> Skills { get; set; }

        public string AvatarFileName { get; set; }

        public string ResumeFileName { get; set; }

        public IList<string> DocumentFileNames { get; set; }
    }
}