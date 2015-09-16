using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Optimization;
using RecruitmentManagementSystem.App.ViewModels;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Data.Interfaces;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class CandidateController : BaseController
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public ActionResult Index()
        {
            var results = _candidateRepository.FindAll();

            var resultViewModel = results.Select(result => new CandidateViewModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Others = result.Others,
                Website = result.Website
            }).ToList();

            ViewData["CandidateNo"] = resultViewModel.Count;

            return View(resultViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = _candidateRepository.Find(x => x.Id == id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CandidateViewModel candidate)
        {
            if (!ModelState.IsValid) return View(candidate);

            _candidateRepository.Insert(new Candidate
            {
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = _candidateRepository.Find(x => x.Id == id);

            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, UserId")] Candidate candidate)
        {
            if (!ModelState.IsValid) return View(candidate);

            _candidateRepository.Update(candidate);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = _candidateRepository.Find(x => x.Id == id);

            if (candidate == null)
            {
                return HttpNotFound();
            }

            return View(candidate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var candidate = _candidateRepository.Find(x => x.Id == id);
            _candidateRepository.Delete(candidate);

            return RedirectToAction("Index");
        }
    }
}