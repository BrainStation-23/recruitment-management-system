using System.Linq;
using System.Web.Mvc;
using RecruitmentManagementSystem.App.ViewModels;
using RecruitmentManagementSystem.App.ViewModels.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionCategoryController : BaseController
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionCategoryController(IQuestionCategoryRepository questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreateViewModel question)
        {
            if (!ModelState.IsValid) return View(question);

            _questionCategoryRepository.Insert(new QuestionCategory
            {
                Name = question.Name,
                Description = question.Description
            });

            _questionCategoryRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult List()
        {
            var results = _questionCategoryRepository.FindAll();

            var resultsViewModel = results.Select(result => new QuestionViewModel
            {
                Id = result.Id
            }).ToList();

            return View(resultsViewModel);
        }
	}
}