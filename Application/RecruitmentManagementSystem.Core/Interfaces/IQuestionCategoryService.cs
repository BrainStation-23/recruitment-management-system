using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Question;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionCategoryService
    {
        IEnumerable<QuestionCategoryDto> GetPagedList();

        void Insert(QuestionCategoryDto model);

        void Update(QuestionCategoryDto model);
    }
}
