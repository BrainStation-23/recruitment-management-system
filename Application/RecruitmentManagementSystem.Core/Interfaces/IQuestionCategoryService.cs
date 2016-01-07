using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IQuestionCategoryService
    {
        IEnumerable<Model.QuestionCategory> GetPagedList();

        void Insert(Model.QuestionCategory model);

        void Update(Model.QuestionCategory model);
    }
}
