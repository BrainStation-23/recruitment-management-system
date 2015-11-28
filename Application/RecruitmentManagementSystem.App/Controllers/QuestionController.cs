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
        private readonly IFileRepository _fileRepository;

        public QuestionController(IQuestionRepository questionRepository, IFileRepository fileRepository)
        {
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
        }

        private static QuestionViewModel ViewModelQuestion(Question question)
        {
            var viewModel = new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text,
                QuestionType = question.QuestionType,
                Notes = question.Notes,
                CategoryId = question.CategoryId,
                Category = question.Category.Name
            };
            return viewModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionViewModel>();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var viewModel =
                _questionRepository.FindAll().ProjectTo<QuestionViewModel>().SingleOrDefault(x => x.Id == id);

            if (Request.IsAjaxRequest())
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(QuestionCreateViewModel viewModel)
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

        private ICollection<File> ManageFiles(HttpFileCollectionBase fileCollection)
        {
            var files = new List<File>();

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index] == null || fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                var uploaded = UploadFile(Request.Files[index]);

                if (!String.IsNullOrEmpty(uploaded.FilePath))
                {
                    var file = new File
                    {
                        Name = uploaded.FileName,
                        MimeType = uploaded.FileBase.ContentType,
                        Size = uploaded.FileBase.ContentLength,
                        RelativePath = uploaded.FilePath + uploaded.FileName,
                        FileType = FileType.Document,
                        CreatedBy = User.Identity.GetUserId(),
                        UpdatedBy = User.Identity.GetUserId()
                    };

                    files.Add(file);
                }
            }

            return files;
        }

        private UploadConfig UploadFile(HttpPostedFileBase fileBase)
        {
            if (fileBase == null || fileBase.ContentLength <= 0) return new UploadConfig();

            var fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetFileName(fileBase.FileName));

            const string filePath = FilePath.DocumentRelativePath;

            var uploadConfig = new UploadConfig
                               {
                                   FileBase = fileBase,
                                   FileName = fileName,
                                   FilePath = filePath
                               };

            FileHelper.SaveFile(uploadConfig);

            return uploadConfig;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var question = _questionRepository.Find(x => x.Id == model.Id);

            _questionRepository.Update(question);
            _questionRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var question = _questionRepository.Find(x => x.Id == id);
            if (question == null) return new HttpNotFoundResult();
            return View(ViewModelQuestion(question));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionRepository.Delete(id);
            _questionRepository.Save();

            return RedirectToAction("Index");
        }
    }
}