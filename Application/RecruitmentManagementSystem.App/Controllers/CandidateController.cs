using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
        private readonly IProjectRepository _projectRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateController(ICandidateRepository candidateRepository, IFileRepository fileRepository,
            IEducationRepository educationRepository, IExperienceRepository experienceRepository,
            IProjectRepository projectRepository, ISkillRepository skillRepository)
        {
            _candidateRepository = candidateRepository;
            _fileRepository = fileRepository;
            _educationRepository = educationRepository;
            _experienceRepository = experienceRepository;
            _projectRepository = projectRepository;
            _skillRepository = skillRepository;
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
                return Json(ModelState.Values.SelectMany(v => v.Errors));
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
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Present = item.CurrentlyPresent,
                        Activites = item.Activites,
                        Notes = item.Notes,
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
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Present = item.Present,
                        Description = item.Description,
                        Notes = item.Notes,
                        CandidateId = candidate.Id
                    });
                }
                _experienceRepository.Save();
            }

            if (model.Projects != null)
            {
                foreach (var item in model.Projects)
                {
                    _projectRepository.Insert(new Project
                    {
                        Title = item.Title,
                        Url = item.Url,
                        Description = item.Description,
                        CandidateId = candidate.Id
                    });
                }
                _projectRepository.Save();
            }

            if (model.Skills != null)
            {
                foreach (var item in model.Skills)
                {
                    _skillRepository.Insert(new Skill
                    {
                        Name = item.Name,
                        CandidateId = candidate.Id
                    });
                }
                _projectRepository.Save();
            }

            ManageFiles(candidate.Id, Request.Files, model);

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

        #region Private Methods

        private void ManageFiles(int candidateId, HttpFileCollectionBase fileCollection, CandidateViewModel viewModel)
        {
            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index] == null || fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                if (fileCollection[index].FileName == viewModel.AvatarFileName)
                {
                    UploadFile(candidateId, fileCollection[index], FileType.Avatar);
                }
                else if (fileCollection[index].FileName == viewModel.ResumeFileName)
                {
                    UploadFile(candidateId, Request.Files[index], FileType.Resume);
                }
                else if (viewModel.DocumentFileNames.Contains(fileCollection[index].FileName))
                {
                    UploadFile(candidateId, Request.Files[index], FileType.Document);
                }
            }
        }

        private void UploadFile(int candidateId, HttpPostedFileBase fileBase, FileType fileType)
        {
            if (fileBase == null || fileBase.ContentLength <= 0) return;

            var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(fileBase.FileName));

            var filePath = string.Empty;

            switch (fileType)
            {
                case FileType.Avatar:
                    filePath = FilePath.AvatarRelativePath;
                    break;
                case FileType.Resume:
                    filePath = FilePath.ResumeRelativePath;
                    break;
                case FileType.Document:
                    filePath = FilePath.DocumentRelativePath;
                    break;
            }

            FileHelper.SaveFile(new UploadConfig
            {
                FileBase = fileBase,
                FileName = fileName,
                FilePath = filePath
            });

            var file = new File
            {
                Name = fileName,
                MimeType = fileBase.ContentType,
                Size = fileBase.ContentLength,
                RelativePath = filePath + fileName,
                FileType = fileType,
                CandidateId = candidateId,
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            _fileRepository.Insert(file);
            _fileRepository.Save();
        }

        #endregion
    }
}