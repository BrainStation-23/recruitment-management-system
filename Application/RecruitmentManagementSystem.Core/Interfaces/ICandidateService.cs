using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Candidate;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateModel> GetPagedList();

        void Insert(CandidateCreateModel model);

        void Update(CandidateCreateModel model);
    }
}