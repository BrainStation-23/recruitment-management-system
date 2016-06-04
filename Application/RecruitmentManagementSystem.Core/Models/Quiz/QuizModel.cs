using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Course;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Quiz
{
    public class QuizModel : BaseModel, IMapFrom<Model.Quiz>
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Required]
        public int CourseId { get; set; }

        public CourseModel Course { get; set; }

        public ICollection<QuizPageCreateModel> QuizPages { get; set; }
        public QuizModel()
        {
            QuizPages = new List<QuizPageCreateModel>();
        }
    }
}