using AutoMapper;

namespace RecruitmentManagementSystem.Core.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}