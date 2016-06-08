using RecruitmentManagementSystem.Core.Models.Question;
using System.Collections.Generic;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<QuestionModel> GetPagedList();

        void Insert(QuestionModel model);
    }
}
