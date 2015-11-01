using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class InstitutionViewModel : IMapFrom<Model.Institution>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }
    }
}