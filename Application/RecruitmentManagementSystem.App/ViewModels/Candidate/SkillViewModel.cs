using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class SkillViewModel : IMapFrom<Model.Skill>
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }
    }
}