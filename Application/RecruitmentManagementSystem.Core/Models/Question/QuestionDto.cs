using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;
using System.Collections.Generic;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class QuestionDto : QuestionBase, IMapFrom<Model.Question>
    {
        public ICollection<FileDto> Files { get; set; }

        public QuestionCategory Category { get; set; }
    }
}
