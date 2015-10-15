using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
        private readonly IChoiceRepository _choiceRepository;

        public QuestionController(IQuestionRepository questionRepository,
            IQuestionCategoryRepository questionCategoryRepository, IChoiceRepository choiceRepository)
        {
            _questionRepository = questionRepository;
            _questionCategoryRepository = questionCategoryRepository;
            _choiceRepository = choiceRepository;
        }

        private QuestionViewModel ViewModelQuestion(Question question)
        {
            var viewModel = new QuestionViewModel
            {
                Id = question.Id,
                Title = question.Title,
                QuestionType = question.Type,
                Notes = question.Notes
            };
            return viewModel;
        }

        public ActionResult Index()
        {
            var results = _questionRepository.FindAll();

            var resultViewModel = results.ToList().Select(result => new QuestionViewModel
            {
                Id = result.Id,
                Title = result.Title,
                QuestionType = result.Type,
                Notes = result.Notes,
                CategoryId = result.CategoryId,
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
            var model = new QuestionCreateViewModel(categories.ToList());

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionCreateViewModel question)
        {
            if (!ModelState.IsValid)
            {
                var questionViewModel = new QuestionCreateViewModel(_questionCategoryRepository.FindAll().ToList())
                {
                    Title = question.Title,
                    Notes = question.Notes,
                    CategoryId = question.CategoryId
                };
                return View(questionViewModel);
            }

            _questionRepository.Insert(new Question
            {
                Title = question.Title,
                Type = question.QuestionType,
                Notes = question.Notes,
                CategoryId = question.CategoryId,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            });

            _questionRepository.Save();

            if (question.QuestionType == QuestionType.Mcq)
            {
                foreach (var item in question.Choices)
                {
                    _choiceRepository.Insert(new Choice
                    {
                        Text = item
                    });
                }

                _choiceRepository.Save();


                //if (Request.IsAjaxRequest())
                //{
                //    return Json(new {Sucess = "Question added successfully."});
                //}
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();

            var categories = _questionCategoryRepository.FindAll();
            ViewBag.categories = categories;

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
                Title = question.Title,
                Type = question.QuestionType,
                Notes = question.Notes,
                CategoryId = question.CategoryId
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