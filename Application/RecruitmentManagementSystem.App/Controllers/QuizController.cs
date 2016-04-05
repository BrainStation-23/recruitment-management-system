using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Models.Quiz;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuizController : BaseController
    {
        private readonly IQuizService _quizService;
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository, IQuizService quizService)
        {
            _quizService = quizService;
            _quizRepository = quizRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(QuizModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            var quizId = _quizService.CreateQuiz(model);

            return new EnhancedJsonResult(quizId);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = _quizRepository.FindAll().ProjectTo<QuizModel>().SingleOrDefault(x => x.Id == id);
            if (Request.IsAjaxRequest())
            {
                return new EnhancedJsonResult(viewModel, JsonRequestBehavior.AllowGet);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(QuizModel model)
        {
            var entity = _quizRepository.Find(model.Id);

            if (entity == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "Question not found.");
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _quizService.Update(model);

            return Json(null);
        }
    }
}