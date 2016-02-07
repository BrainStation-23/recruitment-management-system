using RecruitmentManagementSystem.Core.Interfaces;
using System.Collections.Generic;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly IModelFactory _modelFactory;

        public QuestionCategoryService(IQuestionCategoryRepository questionCategoryRepository, IModelFactory modelFactory)
        {
            _questionCategoryRepository = questionCategoryRepository;
            _modelFactory = modelFactory;
        }

        public IEnumerable<QuestionCategoryModel> GetPagedList()
        {
            var model = _questionCategoryRepository.FindAll().ProjectTo<QuestionCategoryModel>();

            return model;
        }

        public void Insert(QuestionCategoryModel model)
        {
            var entry = _modelFactory.MapToDomain<QuestionCategoryModel, Model.QuestionCategory>(model, null);

            _questionCategoryRepository.Insert(entry);

            _questionCategoryRepository.Save();
        }

        public void Update(QuestionCategoryModel model)
        {
            QuestionCategory entry = _questionCategoryRepository.Find(qc => qc.Id == model.Id);

            QuestionCategory updatedEntry = _modelFactory.MapToDomain(model, entry);

            _questionCategoryRepository.Update(updatedEntry);

            _questionCategoryRepository.Save();
        }
    }
}