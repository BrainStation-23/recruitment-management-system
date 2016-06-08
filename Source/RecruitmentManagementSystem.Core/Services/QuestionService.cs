using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
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

        public QuestionService(IModelFactory modelFactory, IQuestionRepository questionRepository, IFileRepository fileRepository)
        {
            _modelFactory = modelFactory;
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
        }

        public IEnumerable<QuestionModel> GetPagedList()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionModel>();

            return model;
        }

        public void Insert(QuestionModel model)
        {
            var entity = _modelFactory.MapToDomain<QuestionModel, Question>(model, null);

            entity.Choices = _modelFactory.MapToDomain<ChoiceModel, Choice>(model.Choices);

            entity.Files = ManageFiles(model);

            _questionRepository.Insert(entity);

            _questionRepository.Save();
        }

        #region Private methodes

        private ICollection<File> ManageFiles(QuestionModel model)
        {
            var files = new List<File>();
            const FileType fileType = FileType.Document;
            var fileCollection = HttpContext.Current.Request.Files;

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                var uploadConfig = FileHelper.Upload(fileCollection[index], fileType);

                if (uploadConfig.FileBase == null) continue;

                var existingRecords = _fileRepository.FindAll(x => x.Question.Id == model.Id).Select(x => new
                {
                    x.Id,
                    x.RelativePath
                }).ToList();

                /*foreach (var record in existingRecords)
                {
                    FileHelper.Delete(record.RelativePath);
                    _fileRepository.Delete(record.Id);
                }

                _fileRepository.Save();*/

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
