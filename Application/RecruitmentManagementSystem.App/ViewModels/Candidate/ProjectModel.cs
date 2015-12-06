using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class ProjectModel : IMapFrom<Model.Project>
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
    }
}