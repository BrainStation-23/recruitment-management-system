using System.Web.Mvc;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}