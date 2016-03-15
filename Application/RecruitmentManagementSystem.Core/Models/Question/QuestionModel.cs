using AutoMapper;
using RecruitmentManagementSystem.Core.Mappings;

namespace RecruitmentManagementSystem.Core.Models.Question
{
    public class QuestionModel : QuestionBase, IHaveCustomMappings
    {
        public string Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Model.Question, QuestionModel>()
                .ForMember(m => m.Category, opt => opt.MapFrom(u => u.Category.Name))
                .ForMember(m => m.Answers, opt => opt.MapFrom(u => u.Answers))
                .ForMember(m => m.Files, opt => opt.MapFrom(u => u.Files));
        }
    }
}