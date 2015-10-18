using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
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
        private readonly IChoiceRepository _choiceRepository;

        public QuestionController(IQuestionRepository questionRepository,
            IChoiceRepository choiceRepository)
        {
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
        }

        private static QuestionViewModel ViewModelQuestion(Question question)
        {
            var viewModel = new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text,
                QuestionType = question.QuestionType,
                Notes = question.Notes,
                CategoryId = question.CategoryId,
                Category = question.Category.Name
            };
            return viewModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _questionRepository.FindAll().Project().To<QuestionViewModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel =
                _questionRepository.FindAll().Project().To<QuestionViewModel>().SingleOrDefault(x => x.Id == id);

            if (Request.IsAjaxRequest())
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionCreateViewModel question)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(ModelState);
            }

            _questionRepository.Insert(new Question
            {
                Text = question.Text,
                QuestionType = question.QuestionType,
                Answer = question.Answer,
                Notes = question.Notes,
                CategoryId = question.CategoryId,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            });

            _questionRepository.Save();

            if (question.QuestionType != QuestionType.MCQ) return Json(null);

            foreach (var item in question.Choices)
            {
                _choiceRepository.Insert(new Choice
                {
                    Text = item.Text,
                    IsValid = item.IsValid
                });
            }

            _choiceRepository.Save();

            return Json(null);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var question = _questionRepository.Find(x => x.Id == model.Id);

            _questionRepository.Update(question);
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