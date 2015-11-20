using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.App.ViewModels.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

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
            var model = _questionRepository.FindAll().ProjectTo<QuestionViewModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionViewModel>().SingleOrDefault(x => x.Id == id);

            if (Request.IsAjaxRequest())
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            var question = new Question
            {
                Text = viewModel.Text,
                QuestionType = viewModel.QuestionType,
                Answer = viewModel.Answer,
                Notes = viewModel.Notes,
                CategoryId = viewModel.CategoryId,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            _questionRepository.Insert(question);

            _questionRepository.Save();

            if (viewModel.QuestionType != QuestionType.MCQ) return Json(null);

            foreach (var item in viewModel.Choices)
            {
                _choiceRepository.Insert(new Choice
                {
                    Text = item.Text,
                    IsValid = item.IsValid,
                    QuestionId = question.Id
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

        [HttpGet]
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