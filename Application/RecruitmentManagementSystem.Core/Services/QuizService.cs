using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Quiz;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly IModelFactory _modelFactory;
        private readonly IQuizRepository _quizRepository;

        public QuizService(IModelFactory modelFactory, IQuizRepository quizRepository)
        {
            _modelFactory = modelFactory;
            _quizRepository = quizRepository;
        }

        public void CreateQuiz(QuizModel model)
        {
            var entity = _modelFactory.MapToDomain<QuizModel, Quiz>(model, null);
            entity.QuizPages = new List<QuizPage>();

            foreach (var quizPageCreateModel in model.QuizPages)
            {
                var page = _modelFactory.MapToDomain<QuizPageCreateModel, QuizPage>(quizPageCreateModel, null);
                page.QuizQuestions = _modelFactory.MapToDomain<QuizQuestionCreateModel, QuizQuestion>(quizPageCreateModel.QuizQuestions);

                entity.QuizPages.Add(page);
            }

            _quizRepository.Insert(entity);
            _quizRepository.Save();
        }
    }
}