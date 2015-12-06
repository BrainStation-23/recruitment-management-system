using AutoMapper;
using RecruitmentManagementSystem.App.Infrastructure.Mappings;

namespace RecruitmentManagementSystem.App.ViewModels.Question
{
    public class QuestionViewModel : BaseQuestionViewModel, IHaveCustomMappings
    {
        public string Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Model.Question, QuestionViewModel>()
                .ForMember(m => m.Category, opt => opt.MapFrom(u => u.Category.Name))
                .ForMember(m => m.Choices, opt => opt.MapFrom(u => u.Choices))
                .ForMember(m => m.Files, opt => opt.MapFrom(u => u.Files));
        }
    }
}