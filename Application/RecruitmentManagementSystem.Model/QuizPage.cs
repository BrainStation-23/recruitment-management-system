using System.Collections.Generic;

namespace RecruitmentManagementSystem.Model
{
    public class QuizPage : BaseEntity
    {
        public int DisplayOrder { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}