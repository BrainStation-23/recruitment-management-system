using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.App.Infrastructure.Constants;
using RecruitmentManagementSystem.App.Infrastructure.Helpers;
using RecruitmentManagementSystem.App.ViewModels.Candidate;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Data.Interfaces;
using File = RecruitmentManagementSystem.Model.File;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class CandidateController : BaseController
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IFileRepository _fileRepository;
        //private readonly IInstitutionRepository _institutionRepository;

        public CandidateController(ICandidateRepository candidateRepository, IFileRepository fileRepository,
            IEducationRepository educationRepository)
        {
            _candidateRepository = candidateRepository;
            _fileRepository = fileRepository;
            _educationRepository = educationRepository;
            //_institutionRepository = institutionRepository;
        }

        public ActionResult Index()
        {
            var model = _candidateRepository.FindAll().ProjectTo<CandidateViewModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CandidateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Succeeded = false});
            }

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

            foreach (var item in model.Educations)
            {
                _educationRepository.Insert(new Education
                {
                    Degree = item.Degree,
                    FieldOfStudy = item.FieldOfStudy,
                    InstitutionId = item.InstitutionId,
                    CandidateId = candidate.Id
                });
            }

            _educationRepository.Save();

            if (model.Avatar != null && model.Avatar.ContentLength > 0)
            {
                var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(model.Avatar.FileName));

                FileHelper.UploadFile(new UploadConfig
                {
                    FileBase = model.Avatar,
                    FileName = fileName,
                    FilePath = FilePath.AvatarRelativePath
                });

                var file = new File
                {
                    Name = fileName,
                    MimeType = model.Avatar.ContentType,
                    Size = model.Avatar.ContentLength,
                    RelativePath = FilePath.AvatarRelativePath + fileName,
                    FileType = FileType.Avatar,
                    CandidateId = candidate.Id,
                    CreatedBy = User.Identity.GetUserId(),
                    UpdatedBy = User.Identity.GetUserId()
                };

                _fileRepository.Insert(file);
                _fileRepository.Save();
            }

            if (model.Resume != null && model.Resume.ContentLength > 0)
            {
                var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(model.Resume.FileName));

                FileHelper.UploadFile(new UploadConfig
                {
                    FileBase = model.Resume,
                    FileName = fileName,
                    FilePath = FilePath.ResumeRelativePath
                });

                var file = new File
                {
                    Name = fileName,
                    MimeType = model.Resume.ContentType,
                    Size = model.Resume.ContentLength,
                    RelativePath = FilePath.ResumeRelativePath + fileName,
                    FileType = FileType.Resume,
                    CandidateId = candidate.Id,
                    CreatedBy = User.Identity.GetUserId(),
                    UpdatedBy = User.Identity.GetUserId()
                };

                _fileRepository.Insert(file);
                _fileRepository.Save();
            }

            return Json(new { Succeeded = true });
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model =
                _candidateRepository.FindAll().ProjectTo<CandidateViewModel>().SingleOrDefault(x => x.Id == id);

            if (model == null) return new HttpNotFoundResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var model =
                _candidateRepository.FindAll().ProjectTo<CandidateViewModel>().SingleOrDefault(x => x.Id == id);

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
            var model =
                _candidateRepository.FindAll().ProjectTo<CandidateViewModel>().SingleOrDefault(x => x.Id == id);

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