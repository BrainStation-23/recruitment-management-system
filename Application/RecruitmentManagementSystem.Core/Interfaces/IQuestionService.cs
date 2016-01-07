using RecruitmentManagementSystem.Core.Models.Question;
using System.Collections.Generic;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<QuestionDto> GetPagedList();

        void Insert(QuestionCreateDto model);

        void Update(QuestionCreateDto model);
    }
}
