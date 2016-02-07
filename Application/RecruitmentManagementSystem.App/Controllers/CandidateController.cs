using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Models.Candidate;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Data.Interfaces;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class CandidateController : BaseController
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateRepository candidateRepository, ICandidateService candidateService)
        {
            _candidateRepository = candidateRepository;
            _candidateService = candidateService;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _candidateService.GetPagedList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _candidateService.Insert(model);

            return new JsonResult(null);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                ViewData["Id"] = id;
                return View();
            }

            var model = _candidateRepository.FindAll().ProjectTo<CandidateModel>().SingleOrDefault(x => x.Id == id);

            if (model == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                ModelState.AddModelError("", "Candidate not found.");
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors), JsonRequestBehavior.AllowGet);
            }

            model.Avatar = model.Files.SingleOrDefault(x => x.FileType == FileType.Avatar);
            model.Resume = model.Files.SingleOrDefault(x => x.FileType == FileType.Resume);
            model.Files = null;

            return new JsonResult(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateCreateModel model)
        {
            var entity = _candidateRepository.Find(x => x.Id == model.Id);

            if (entity == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                ModelState.AddModelError("", "Candidate not found.");
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _candidateService.Update(model);

            return Json(null);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var model =
                _candidateRepository.FindAll().ProjectTo<CandidateModel>().SingleOrDefault(x => x.Id == id);

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _candidateRepository.Delete(id);
            _candidateRepository.Save();

            return RedirectToAction("List");
        }
    }
}