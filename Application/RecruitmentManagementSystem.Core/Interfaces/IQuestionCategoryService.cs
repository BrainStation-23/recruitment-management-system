using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Question;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionCategoryService
    {
        IEnumerable<QuestionCategory> GetPagedList();

        void Insert(QuestionCategory model);

        void Update(QuestionCategory model);
    }
}
