using System.Linq;
using System.Net;
using System.Web.Mvc;
using RecruitmentManagementSystem.App.Infrastructure.ActionResults;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Models.Quiz;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuizController : BaseController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
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
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new EnhancedJsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _quizService.CreateQuiz(model);

            return new EnhancedJsonResult(null);
        }
    }
}