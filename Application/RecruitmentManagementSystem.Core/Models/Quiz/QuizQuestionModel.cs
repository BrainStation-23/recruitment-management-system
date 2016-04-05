using Microsoft.Build.Framework;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Quiz
{
    public class QuizQuestionCreateModel : BaseModel, IMapFrom<Model.QuizQuestion>
    {
        public int QuizId { get; set; }

        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }

        [Required]
        public decimal Point { get; set; }

        public int DisplayOrder { get; set; }
    }
}