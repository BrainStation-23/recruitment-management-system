using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.App.Infrastructure.Constants;
using RecruitmentManagementSystem.App.Infrastructure.Helpers;
using RecruitmentManagementSystem.App.ViewModels.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;
using File = RecruitmentManagementSystem.Model.File;
using JsonResult = RecruitmentManagementSystem.App.Infrastructure.ActionResults.JsonResult;

namespace RecruitmentManagementSystem.App.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

            if (Request.IsAjaxRequest())
            {
                return new JsonResult(viewModel, JsonRequestBehavior.AllowGet);
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreateModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            var question = new Question
            {
                Text = viewModel.Text,
                QuestionType = viewModel.QuestionType,
                Answer = viewModel.Answer,
                Notes = viewModel.Notes,
                CategoryId = viewModel.CategoryId,
                Choices = viewModel.Choices,
                Files = ManageFiles(Request.Files),
                CreatedBy = User.Identity.GetUserId(),
                UpdatedBy = User.Identity.GetUserId()
            };

            _questionRepository.Insert(question);

            _questionRepository.Save();

            return Json(null);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["Id"] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionCreateModel model)
        {

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new JsonResult(ModelState.Values.SelectMany(v => v.Errors));
            }

            _questionRepository.Update(new Question
            {
                Id = model.Id,
                Text = model.Text,
                QuestionType = model.QuestionType,
                Answer = model.Answer,
                Notes = model.Notes,
                CategoryId = model.CategoryId,
                Choices = model.Choices,
                Files = ManageFiles(Request.Files),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = User.Identity.GetUserId()
            });
            _questionRepository.Save();
            return Json(null);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();
            //return View(ViewModelQuestion(question));
            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionModel>().SingleOrDefault(x => x.Id == id);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionRepository.Delete(_questionRepository.Find(x => x.Id == id));
            _questionRepository.Save();

            return RedirectToAction("List");
        }

        #region Private Methods

        private ICollection<File> ManageFiles(HttpFileCollectionBase fileCollection)
        {
            var files = new List<File>();

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index] == null || fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                var uploadConfig = UploadFile(Request.Files[index]);

                if (uploadConfig.FileBase == null) continue;

                var file = new File
                {
                    Name = uploadConfig.FileName,
                    MimeType = uploadConfig.FileBase.ContentType,
                    Size = uploadConfig.FileBase.ContentLength,
                    RelativePath = uploadConfig.FilePath + uploadConfig.FileName,
                    FileType = FileType.Document,
                    CreatedBy = User.Identity.GetUserId(),
                    UpdatedBy = User.Identity.GetUserId()
                };

                files.Add(file);
            }

            return files;
        }

        private static UploadConfig UploadFile(HttpPostedFileBase fileBase)
        {
            var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(fileBase.FileName));

            const string filePath = FilePath.DocumentRelativePath;

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