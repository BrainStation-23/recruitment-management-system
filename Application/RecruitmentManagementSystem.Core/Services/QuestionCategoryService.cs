using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Web;
using AutoMapper;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Question;

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

        public IEnumerable<QuestionCategory> GetPagedList()
        {
            var model = _questionCategoryRepository.FindAll().ProjectTo<QuestionCategory>();

            return model;
        }

        public void Insert(QuestionCategory model)
        {
            var entry = _modelFactory.MapToDomain<QuestionCategory, Model.QuestionCategory>(model, null);

            _questionCategoryRepository.Insert(entry);

            _questionCategoryRepository.Save();
        }

        public void Update(QuestionCategory model)
        {
            throw new NotImplementedException();
        }
    }
}
