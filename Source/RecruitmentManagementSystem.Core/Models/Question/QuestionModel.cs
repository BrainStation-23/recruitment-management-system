using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class QuestionModel : BaseModel, IMapFrom<Model.Question>
    {
        public QuestionModel()
        {
            Choices = new List<ChoiceModel>();
            Files = new List<FileModel>();
        }

        [Required]
        [AllowHtml]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        public ICollection<ChoiceModel> Choices { get; set; }

        public ICollection<FileModel> Files { get; set; }

        [AllowHtml]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Answer { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public decimal DefaultPoint { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public QuestionCategoryModel Category { get; set; }
    }
}
