using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class AnswerModel : BaseModel, IMapFrom<Answer>
    {
        public AnswerModel()
        {
            
        }

        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string AnswerText { get; set; }

        public QuestionModel Question { get; set; }
    }
}