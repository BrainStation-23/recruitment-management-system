using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        QuestionCategoryService(IQuestionCategoryRepository questionCategoryRepository)
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
