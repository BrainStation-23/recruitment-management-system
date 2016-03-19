using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Mappings;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionService _questionService;
        private readonly IModelFactory _modelFactory;

        public QuestionController(IQuestionRepository questionRepository, IFileRepository fileRepository,
            IAnswerRepository answerRepository, IQuestionService questionService, IModelFactory modelFactory)
        {
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
            _answerRepository = answerRepository;
            _questionService = questionService;
            _modelFactory = modelFactory;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _questionService.GetPagedList();

            if (Request.IsAjaxRequest())
            {
                return new EnhancedJsonResult(model, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionCreateModel model)
        {
            var entity = _questionRepository.Find(model.Id);

            if (entity == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                ModelState.AddModelError("", "Question not found.");
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _questionService.Update(model);

            return Json(null);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();

            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

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