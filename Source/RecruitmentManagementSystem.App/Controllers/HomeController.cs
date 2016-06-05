using System.Web.Mvc;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}