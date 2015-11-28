using System.Web.Mvc;
using RecruitmentManagementSystem.Data.Interfaces;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class JobPositionController : BaseController
    {
        private readonly IJobPositionRepository _jobPositionRepository;

        public JobPositionController(IJobPositionRepository jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _jobPositionRepository.FindAll();
            return new JsonResult(model, JsonRequestBehavior.AllowGet);
        }
    }
}