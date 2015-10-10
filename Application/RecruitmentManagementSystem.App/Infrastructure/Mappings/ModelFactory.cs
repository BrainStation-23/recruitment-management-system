using AutoMapper;
using RecruitmentManagementSystem.App.ViewModels.Candidate;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.App.Infrastructure.Mappings
{
    public class ModelFactory
    {
        public CandidateViewModel Map(Candidate candidate)
        {
            Mapper.CreateMap<Candidate, CandidateViewModel>();
            return Mapper.Map<Candidate, CandidateViewModel>(candidate);
        }
    }
}