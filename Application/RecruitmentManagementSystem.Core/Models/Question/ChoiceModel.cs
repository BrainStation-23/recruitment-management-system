using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class ChoiceModel : BaseModel, IMapFrom<Choice>
    {
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public bool IsValid { get; set; }

        public QuestionModel Question { get; set; }
    }
}