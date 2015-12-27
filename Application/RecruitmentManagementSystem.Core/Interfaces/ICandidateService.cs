using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Candidate;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<CandidateDto> GetPagedList();

        void Insert(CandidateCreateDto model);

        void Update(CandidateCreateDto model);
    }
}