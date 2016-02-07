using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Quiz
{
    public class QuizPageCreateModel : BaseModel
    {
        public int DisplayOrder { get; set; }

        public int QuizId { get; set; }

        public ICollection<QuizQuestionCreateModel> QuizQuestions { get; set; }
    }
}