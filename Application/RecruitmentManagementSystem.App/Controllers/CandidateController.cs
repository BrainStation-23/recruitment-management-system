using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

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

        [HttpGet]
        public ActionResult List()
        {
            var model = _candidateRepository.FindAll().ProjectTo<CandidateModel>();

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

            var candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Others = model.Others,
                Website = model.Website,
                JobPositionId = model.JobPositionId,
                Files = ManageFiles(Request.Files, model),
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            if (model.Educations != null)
            {
                var educations = new Collection<Education>();

                foreach (var item in model.Educations)
                {
                    educations.Add(new Education
                    {
                        Degree = item.Degree,
                        FieldOfStudy = item.FieldOfStudy,
                        InstitutionId = item.InstitutionId,
                        Grade = item.Grade,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Present = item.CurrentlyPresent,
                        Activities = item.Activities,
                        Notes = item.Notes
                    });
                }
                candidate.Educations = educations;
            }

            if (model.Experiences != null)
            {
                var experiences = new Collection<Experience>();

                foreach (var item in model.Experiences)
                {
                    experiences.Add(new Experience
                    {
                        Organization = item.Organization,
                        JobTitle = item.JobTitle,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Present = item.Present,
                        Description = item.Description,
                        Notes = item.Notes
                    });
                }
                candidate.Experiences = experiences;
            }

            if (model.Projects != null)
            {
                var projects = new Collection<Project>();

                foreach (var item in model.Projects)
                {
                    projects.Add(new Project
                    {
                        Title = item.Title,
                        Url = item.Url,
                        Description = item.Description
                    });
                }
                candidate.Projects = projects;
            }

            if (model.Skills != null)
            {
                var skills = new Collection<Skill>();

                foreach (var item in model.Skills)
                {
                    skills.Add(new Skill
                    {
                        Name = item.Name
                    });
                }

                candidate.Skills = skills;
            }

            _candidateRepository.Insert(candidate);
            _candidateRepository.Save();

            return new JsonResult(candidate);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!Request.IsAjaxRequest())
            {
                ViewData["Id"] = id;
                return View();
            }

            var model = _candidateRepository.FindAll().ProjectTo<CandidateModel>().SingleOrDefault(x => x.Id == id);

            ExtendCandidateModel(model);

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
        public ActionResult Edit(CandidateModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _candidateRepository.Update(new Candidate
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Others = model.Others,
                Website = model.Website,
                JobPositionId = model.JobPositionId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = User.Identity.GetUserId()
            });

            _candidateRepository.Save();

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

        #region Private Methods

        private static void ExtendCandidateModel(CandidateModel model)
        {
            model.Avatar = model.Files.SingleOrDefault(x => x.FileType == FileType.Avatar);
            model.Resume = model.Files.SingleOrDefault(x => x.FileType == FileType.Resume);
            model.Files = null;
        }

        private ICollection<File> ManageFiles(HttpFileCollectionBase fileCollection,
            CandidateCreateModel viewModel)
        {
            var files = new List<File>();

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index] == null || fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                var uploadConfig = UploadFile(fileCollection[index], FileType.Avatar);

                if (uploadConfig.FileBase == null) continue;

                var file = new File
                {
                    Name = uploadConfig.FileName,
                    MimeType = uploadConfig.FileBase.ContentType,
                    Size = uploadConfig.FileBase.ContentLength,
                    RelativePath = uploadConfig.FilePath + uploadConfig.FileName,
                    CreatedBy = User.Identity.GetUserId(),
                    UpdatedBy = User.Identity.GetUserId()
                };

                if (fileCollection[index].FileName == viewModel.AvatarFileName)
                {
                    file.FileType = FileType.Avatar;
                }
                else if (fileCollection[index].FileName == viewModel.ResumeFileName)
                {
                    file.FileType = FileType.Resume;
                }
                else if (viewModel.DocumentFileNames.Contains(fileCollection[index].FileName))
                {
                    file.FileType = FileType.Document;
                }

                files.Add(file);
            }

            return files;
        }

        private static UploadConfig UploadFile(HttpPostedFileBase fileBase, FileType fileType)
        {
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

            var uploadConfig = new UploadConfig
            {
                FileBase = fileBase,
                FileName = fileName,
                FilePath = filePath
            };

            try
            {
                FileHelper.SaveFile(uploadConfig);
            }
            catch (Exception)
            {
                return new UploadConfig();
            }

            return uploadConfig;
        }

        #endregion
    }
}