using System.Collections.Generic;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;
using RecruitmentManagementSystem.App.ViewModels.Shared;

namespace RecruitmentManagementSystem.App.ViewModels.Candidate
{
    public class CandidateModel : CandidateBaseModel, IMapFrom<Model.Candidate>
    {
        public ICollection<FileViewModel> Files { get; set; }

        public FileViewModel Avatar { get; set; }

        public FileViewModel Resume { get; set; }
    }
}