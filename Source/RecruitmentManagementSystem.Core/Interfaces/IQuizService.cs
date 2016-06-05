using RecruitmentManagementSystem.Core.Models.Quiz;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuizService
    {
        int CreateQuiz(QuizModel model);
        Quiz GetQuiz(int quizId);
        void Update(QuizModel model);
    }
}