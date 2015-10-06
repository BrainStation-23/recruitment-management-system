using System.Linq;
using System.Web.Mvc;
using RecruitmentManagementSystem.App.ViewModels;
using RecruitmentManagementSystem.App.ViewModels.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionController(IQuestionRepository questionRepository, IQuestionCategoryRepository questionCategoryRepository)
        {
            _questionRepository = questionRepository;
            _questionCategoryRepository = questionCategoryRepository;
        }

        private QuestionViewModel ViewModelQuestion(Question question)
        {
            var viewModel = new QuestionViewModel
            {
                Id = question.Id,
                Tittle = question.Tittle,
                Type = question.Type,
                DisplayType = question.DisplayType,
                Note = question.Note
            };
            return viewModel;
        }

        // GET: /Question/
        public ActionResult Index()
        {
            var results = _questionRepository.FindAll();

            var resultViewModel = results.Select(result => new QuestionViewModel
            {
                Id = result.Id,
                Tittle = result.Tittle,
                Type = result.Type,
                DisplayType = result.DisplayType,
                Note = result.Note,
            }).ToList();

            ViewData["QuestionNo"] = resultViewModel.Count;

            return View(resultViewModel);
        }

        public ActionResult Details(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();
            return View(ViewModelQuestion(question));
        }

        public ActionResult Create()
        {
            var categories = _questionCategoryRepository.FindAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel question)
        {
            if (!ModelState.IsValid) return View(question);

            _questionRepository.Insert(new Question
            {
                Tittle = question.Tittle,
                Type = question.Type,
                DisplayType = question.DisplayType,
                Note = question.Note,
                CategoryId = question.CategoryId

            });

            _questionRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();
            return View(ViewModelQuestion(question));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel question)
        {
            if (!ModelState.IsValid) return View(question);

            _questionRepository.Update(new Question
            {
                Id = question.Id,
                Tittle = question.Tittle,
                Type = question.Type,
                DisplayType = question.DisplayType,
                Note = question.Note
            });

            _questionRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();
            return View(ViewModelQuestion(question));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionRepository.Delete(id);
            _questionRepository.Save();

            return RedirectToAction("Index");
        }
	}
}