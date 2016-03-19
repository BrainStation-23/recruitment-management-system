using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class QuizQuestion : BaseEntity
    {
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public int DisplayOrder { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required]
        public decimal Point { get; set; }
    }
}