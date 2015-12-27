using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Institution
{
    public class InstitutionDto : BaseDto, IMapFrom<Model.Institution>
    {
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string City { get; set; }
    }
}