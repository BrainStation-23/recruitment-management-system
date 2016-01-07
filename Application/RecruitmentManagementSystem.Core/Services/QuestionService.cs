using RecruitmentManagementSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Data.Interfaces;
using AutoMapper.QueryableExtensions;

namespace RecruitmentManagementSystem.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IChoiceRepository _choiceRepository;

        public QuestionService(IQuestionRepository questionRepository, IFileRepository fileRepository, IChoiceRepository choiceRepository)
        {
            _questionRepository = questionRepository;
            _fileRepository = fileRepository;
            _choiceRepository = choiceRepository;
        }

        public IEnumerable<QuestionDto> GetPagedList()
        {
            var model = _questionRepository.FindAll().ProjectTo<QuestionDto>();

            return model;
        }

        public void Insert(QuestionCreateDto model)
        {
            throw new NotImplementedException();
        }

        public void Update(QuestionCreateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
