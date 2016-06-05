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
            Choices = new List<Choice>();
            Files = new List<File>();
            Category = new QuestionCategory();
        }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        public ICollection<Choice> Choices { get; set; }

        public ICollection<File> Files { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Answer { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        public decimal DefaultPoint { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public QuestionCategory Category { get; set; }
    }
}
