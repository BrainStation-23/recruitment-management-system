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
        private readonly IAnswerRepository _answerRepository;

        public QuestionService(IModelFactory modelFactory, IQuestionRepository questionRepository, IFileRepository fileRepository, IAnswerRepository answerRepository)
        {
            _modelFactory = modelFactory;
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
            _answerRepository = answerRepository;
        }

        public IEnumerable<QuestionModel> GetPagedList()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionModel>();

            return model;
        }

        public void Insert(QuestionCreateModel model)
        {
            var entity = _modelFactory.MapToDomain<QuestionCreateModel, Question>(model, null);

            entity.Answers = _modelFactory.MapToDomain<AnswerModel, Answer>(model.Answers);

            entity.Files = ManageFiles(model);

            _questionRepository.Insert(entity);

            _questionRepository.Save();
        }

        public void Update(QuestionCreateModel model)
        {
            var entity = _questionRepository.FindIncluding(q => q.Id == model.Id, q => q.Answers, q => q.Files);

            if (entity == null) return;

            if (model.Answers != null)
            {
                foreach (var choice in model.Answers.Where(c => c.QuestionId == default(int)))
                {
                    choice.QuestionId = model.Id;
                }
            }

            var updatedEntity = _modelFactory.MapToDomain(model, entity);

            updatedEntity.Answers = _modelFactory.MapToDomain<AnswerModel, Answer>(model.Answers);
            updatedEntity.Files = ManageFiles(model);

            _questionRepository.Update(updatedEntity);

            foreach (var choice in entity.Answers.Where(y => model.Answers.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _answerRepository.Delete(choice.Id);
            }

            _questionRepository.Save();
            _answerRepository.Save();

            /*            if (model.DeletableFile != null)
                        {
                            foreach (var file in model.DeletableFile)
                            {
                                _fileRepository.Delete(file.Id);
                                FileHelper.DeleteFile(file);
                            }
                            _fileRepository.Save();
                        }

                        var choices = _choiceRepository.FindAll(x => x.QuestionId == model.Id).ToList();

                        if (choices.Count > 0)
                        {
                            foreach (var choice in choices)
                            {
                                _choiceRepository.Delete(choice);
                            }
                            _choiceRepository.Save();
                        }

                        entity.Text = model.Text;
                        entity.Answer = model.Answer;
                        entity.CategoryId = model.CategoryId;
                        entity.Notes = model.Notes;
                        entity.QuestionType = model.QuestionType;
                        entity.Files = ManageFiles(Request.Files);
                        entity.Choices = _modelFactory.MapToDomain<ChoiceModel, Choice>(model.Choices);

                        _questionRepository.Update(entity);
                        _questionRepository.Save();*/

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