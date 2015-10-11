using System.Collections.Generic;
using System.Web.Mvc;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class QuestionCreateViewModel : BaseQuestionViewModel
    {
        private readonly List<QuestionCategory> _categories;

        public QuestionCreateViewModel()
        {
        }

        public QuestionCreateViewModel(List<QuestionCategory> categories)
        {
            _categories = categories;
        }

        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(_categories, "Id", "Name"); }
        }
    }
}