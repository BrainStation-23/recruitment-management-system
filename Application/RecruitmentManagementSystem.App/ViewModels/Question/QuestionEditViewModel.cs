using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class QuestionEditViewModel : QuestionViewModel
    {
        private readonly List<QuestionCategory> _categories;

        public QuestionEditViewModel()
        {
        }

        public QuestionEditViewModel(List<QuestionCategory> categories)
        {
            _categories = categories;
        }

        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(_categories, "Id", "Name"); }
        }
    }
}