using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Question;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionCategoryService
    {
        IEnumerable<QuestionCategoryModel> GetPagedList();

        void Insert(QuestionCategoryModel model);

        void Update(QuestionCategoryModel model);
    }
}