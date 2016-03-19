using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class ProjectModel : BaseModel, IMapFrom<Model.Project>
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Url]
        public string Url { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public string UserId { get; set; }
    }
}