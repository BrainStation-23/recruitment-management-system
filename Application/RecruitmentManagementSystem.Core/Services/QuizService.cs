using System.Collections.Generic;
using System.Linq;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Course;
using RecruitmentManagementSystem.Core.Models.Quiz;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Data.Repositories;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuizService : IQuizService
    {
        private readonly IModelFactory _modelFactory;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizPagesRepository _quizPagesRepository;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ICourseRepository _courseRepository;

        public QuizService(IModelFactory modelFactory, IQuizRepository quizRepository, IQuizQuestionRepository quizQuestionRepository,
            IQuestionRepository questionRepository, ICourseRepository courseRepository, IQuizPagesRepository quizPagesRepository)
        {
            _modelFactory = modelFactory;
            _quizRepository = quizRepository;
            _quizQuestionRepository = quizQuestionRepository;
            _questionRepository = questionRepository;
            _courseRepository = courseRepository;
            _quizPagesRepository = quizPagesRepository;
        }

        public int CreateQuiz(QuizModel model)
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
            return entity.Id;
        }
        public Quiz GetQuiz(int quizId)
        {
            
            Quiz model = _quizRepository.FindIncluding(a => a.Id == quizId, a => a.Course, a => a.QuizPages);
       
            return model;

        }
        public void Update(QuizModel model)
        {
            var entity = _quizRepository.FindIncluding(q => q.Id == model.Id, q => q.Course, q => q.QuizPages);

            if (entity == null) return;

            entity.Title = model.Title;

            entity.CourseId = model.CourseId;

            #region Manage QuizPages

            foreach (var m in model.QuizPages.Where(m => m.Id <= 0))
            {
                m.QuizId = model.Id;
            }
            foreach (var quizPage in model.QuizPages)
            {
                _quizPagesRepository.InsertOrUpdate(_modelFactory.MapToDomain<QuizPageCreateModel, QuizPage>(quizPage, null));
            }

            foreach (var m in entity.QuizPages.Where(y => model.QuizPages.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _quizPagesRepository.Delete(m.Id);
            }

            _quizPagesRepository.Save();

            #endregion

            _quizRepository.Update(entity);

            _quizRepository.Save();
            
        }
    }
}