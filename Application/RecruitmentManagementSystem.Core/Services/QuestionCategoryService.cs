using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using RecruitmentManagementSystem.Core.Models.Question;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionCategoryService(IQuestionCategoryRepository questionCategoryRepository)
        {
            _questionCategoryRepository = questionCategoryRepository;
        }

        public IEnumerable<QuestionCategory> GetPagedList()
        {
            var model = _questionCategoryRepository.FindAll().ProjectTo<QuestionCategory>();

            return model;
        }

        public void Insert(QuestionCategory model)
        {
            throw new NotImplementedException();
        }

        public void Update(QuestionCategory model)
        {
            throw new NotImplementedException();
        }
    }
}
