using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Models.Candidate
{
    public class CandidateDto : CandidateBase, IMapFrom<Model.Candidate>
    {
        public ICollection<FileDto> Files { get; set; }

        public FileDto Avatar { get; set; }

        public FileDto Resume { get; set; }

        public JobPosition JobPosition { get; set; }
    }
}