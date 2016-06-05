using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Core.Interfaces;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionRepository questionRepository,
            IQuestionService questionService)
        {
            _questionRepository = questionRepository;
            _questionService = questionService;
        }

        [HttpGet]
        public ActionResult List(string categoryName)
        {
            var model = !string.IsNullOrEmpty(categoryName) ? _questionService.GetPagedList().Where(x => x.Category.Name == categoryName) : _questionService.GetPagedList();

            if (Request.IsAjaxRequest())
            {
                return new EnhancedJsonResult(model, JsonRequestBehavior.AllowGet);
            }

            return View(model);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel = _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

            if (Request.IsAjaxRequest())
            {
                return new EnhancedJsonResult(viewModel, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(QuestionCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _questionService.Insert(model);

            return Json(null);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = _questionRepository.FindAll()
                .ProjectTo<QuestionModel>()
                .SingleOrDefault(x => x.Id == id);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();

            var viewModel = _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionRepository.Delete(_questionRepository.Find(x => x.Id == id));
            _questionRepository.Save();

            return RedirectToAction("List");
        }
    }
}
