using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.Core.Models.Institution;
using RecruitmentManagementSystem.Data.Interfaces;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionController(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _institutionRepository.FindAll().ProjectTo<InstitutionDto>();

            return new JsonResult(model, JsonRequestBehavior.AllowGet);
        }
    }
}