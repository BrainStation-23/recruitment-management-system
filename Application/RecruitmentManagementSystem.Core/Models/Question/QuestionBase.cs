using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Model;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class QuestionBase : BaseModel
    {
        public QuestionBase()
        {
            QuestionType = new QuestionType();
            Answers = new List<Answer>();
            Files = new List<File>();
            Category = new QuestionCategory();
        }

        [Required]
        [Display(Name = "Text")]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Answers")]
        public ICollection<Answer> Answers { get; set; }

        public ICollection<File> Files { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Display(Name = "Default Mark")]
        public decimal DefaultPoint { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public QuestionCategory Category { get; set; }
    }
}