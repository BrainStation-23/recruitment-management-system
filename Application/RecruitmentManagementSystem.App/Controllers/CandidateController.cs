using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;
using RecruitmentManagementSystem.App.ViewModels.Candidate;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Data.Interfaces;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class CandidateController : BaseController
    {
        private readonly ModelFactory _modelFactory;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateController(ModelFactory modelFactory, ICandidateRepository candidateRepository)
        {
            _modelFactory = modelFactory;
            _candidateRepository = candidateRepository;
        }

        public ActionResult Index()
        {
            var model = _candidateRepository.FindAll().Project().To<CandidateViewModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Others = model.Others,
                Website = model.Website,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            _candidateRepository.Insert(candidate);
            _candidateRepository.Save();

            return RedirectToAction("Edit", new {id = candidate.Id});
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model = _modelFactory.Map(_candidateRepository.Find(x => x.Id == id));

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var model = _modelFactory.Map(_candidateRepository.Find(x => x.Id == id));

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateViewModel candidate)
        {
            if (!ModelState.IsValid) return View(candidate);

            _candidateRepository.Update(new Candidate
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                Others = candidate.Others,
                Website = candidate.Website
            });

            _candidateRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var model = _modelFactory.Map(_candidateRepository.Find(x => x.Id == id));

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _candidateRepository.Delete(id);
            _candidateRepository.Save();

            return RedirectToAction("Index");
        }
    }
}