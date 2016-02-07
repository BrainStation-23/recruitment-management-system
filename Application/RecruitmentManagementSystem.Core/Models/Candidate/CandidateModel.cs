using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.Candidate
{
    public class CandidateModel : CandidateBase, IMapFrom<Model.Candidate>
    {
        public ICollection<FileModel> Files { get; set; }

        public FileModel Avatar { get; set; }

        public FileModel Resume { get; set; }
    }
}