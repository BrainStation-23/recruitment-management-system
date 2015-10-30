using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
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
        private readonly IExperienceRepository _experienceRepository;

        public CandidateController(ICandidateRepository candidateRepository, IFileRepository fileRepository,
            IEducationRepository educationRepository, IExperienceRepository experienceRepository)
        {
            _candidateRepository = candidateRepository;
            _fileRepository = fileRepository;
            _educationRepository = educationRepository;
            _experienceRepository = experienceRepository;
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
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(null);
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

            if (model.Educations != null)
            {
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
            }

            if (model.Experiences != null)
            {
                foreach (var item in model.Experiences)
                {
                    _experienceRepository.Insert(new Experience
                    {
                        Organization = item.Organization,
                        JobTitle = item.JobTitle,
                        From = item.From,
                        To = item.To,
                        StillWorking = item.StillWorking,
                        Description = item.Description,
                        CandidateId = candidate.Id
                    });
                }
                _experienceRepository.Save();
            }

            if (Request.Files != null && Request.Files.Count > 0)
            {
                var avatar = Request.Files[0];

                if (avatar != null && avatar.ContentLength > 0)
                {
                    var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(avatar.FileName));

                    FileHelper.UploadFile(new UploadConfig
                    {
                        FileBase = avatar,
                        FileName = fileName,
                        FilePath = FilePath.AvatarRelativePath
                    });

                    var file = new File
                    {
                        Name = fileName,
                        MimeType = avatar.ContentType,
                        Size = avatar.ContentLength,
                        RelativePath = FilePath.AvatarRelativePath + fileName,
                        FileType = FileType.Avatar,
                        CandidateId = candidate.Id,
                        CreatedBy = User.Identity.GetUserId(),
                        UpdatedBy = User.Identity.GetUserId()
                    };

                    _fileRepository.Insert(file);
                    _fileRepository.Save();
                }
            }

            return Json(candidate);
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

        [HttpPost]
        public ActionResult UploadResume(CandidateFileUploadViewModel model)
        {
            if (model.File == null || model.File.ContentLength < 0)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(null);
            }

            var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(model.File.FileName));

            FileHelper.UploadFile(new UploadConfig
            {
                FileBase = model.File,
                FileName = fileName,
                FilePath = FilePath.ResumeRelativePath
            });

            var file = new File
            {
                Name = fileName,
                MimeType = model.File.ContentType,
                Size = model.File.ContentLength,
                RelativePath = FilePath.ResumeRelativePath + fileName,
                FileType = FileType.Resume,
                CandidateId = model.CandidateId,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            _fileRepository.Insert(file);
            _fileRepository.Save();

            return Json(null);
        }
    }
}