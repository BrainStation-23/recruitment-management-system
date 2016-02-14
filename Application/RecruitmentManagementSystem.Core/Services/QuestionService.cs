using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.Core.Constants;
using RecruitmentManagementSystem.Core.Helpers;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Model;
using File = RecruitmentManagementSystem.Model.File;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IModelFactory _modelFactory;
        private readonly IQuestionRepository _questionRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IChoiceRepository _choiceRepository;

        public QuestionService(IModelFactory modelFactory, IQuestionRepository questionRepository, IFileRepository fileRepository, IChoiceRepository choiceRepository)
        {
            _modelFactory = modelFactory;
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
            _choiceRepository = choiceRepository;
        }

        public IEnumerable<QuestionModel> GetPagedList()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionModel>();

            return model;
        }

        public void Insert(QuestionCreateModel model)
        {
            var entity = _modelFactory.MapToDomain<QuestionCreateModel, Question>(model, null);

            entity.Choices = _modelFactory.MapToDomain<ChoiceModel, Choice>(model.Choices);

            entity.Files = ManageFiles(model);

            _questionRepository.Insert(entity);

            _questionRepository.Save();


            /*var question = _modelFactory.MapToDomain<QuestionCreateModel, Question>(model, null);
            question.Choices = _modelFactory.MapToDomain<ChoiceModel, Choice>(model.Choices);
            question.Files = ManageFiles(Request.Files);

            _questionRepository.Insert(question);

            _questionRepository.Save();

            return Json(null);*/
        }

        public void Update(QuestionCreateModel model)
        {
            throw new NotImplementedException();
        }

        #region Private methodes

        private ICollection<File> ManageFiles(QuestionCreateModel model)
        {
            var files = new List<File>();
            FileType fileType = FileType.Document;
            var fileCollection = HttpContext.Current.Request.Files;

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                var uploadConfig = FileHelper.Upload(fileCollection[index], fileType);

                if (uploadConfig.FileBase == null) continue;

                var existingRecords = _fileRepository.FindAll(x => x.Candidate.Id == model.Id).Select(x => new
                {
                    x.Id,
                    x.RelativePath
                }).ToList();

                foreach (var record in existingRecords)
                {
                    FileHelper.Delete(record.RelativePath);
                    _fileRepository.Delete(record.Id);
                }

                _fileRepository.Save();

                var file = new File
                {
                    Name = uploadConfig.FileName,
                    MimeType = uploadConfig.FileBase.ContentType,
                    Size = uploadConfig.FileBase.ContentLength,
                    RelativePath = uploadConfig.FilePath + uploadConfig.FileName,
                    FileType = fileType,
                    ObjectState = ObjectState.Added,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                    UpdatedBy = HttpContext.Current.User.Identity.GetUserId()
                };

                files.Add(file);
            }

            return files;
        }

        #endregion
    }
}