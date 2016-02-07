using RecruitmentManagementSystem.Core.Models.Quiz;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuizService
    {
        void CreateQuiz(QuizModel model);
    }
}