using System.Net;
using System.Web.Mvc;
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
            return View(_candidateRepository.FindAll());
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
        public ActionResult Create([Bind(Include = "Id, UserId")] Candidate candidate)
        {
            if (!ModelState.IsValid) return View(candidate);

            _candidateRepository.Insert(candidate);

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