using Microsoft.Build.Framework;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Quiz
{
    public class QuizQuestionCreateModel : BaseModel
    {
        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public decimal Point { get; set; }

        public int DisplayOrder { get; set; }
    }
}