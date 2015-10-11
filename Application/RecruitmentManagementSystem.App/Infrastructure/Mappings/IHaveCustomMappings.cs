using AutoMapper;

namespace RecruitmentManagementSystem.App.Infrastructure.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}