using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using RecruitmentManagementSystem.Core.Models.Question;
using RecruitmentManagementSystem.Core.Models.Quiz;
using RecruitmentManagementSystem.Core.Models.User;
using RecruitmentManagementSystem.Core.Tasks;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            LoadStandardMappings(types);

            LoadCustomMappings(types);

            LoadModelToEntityMappings();
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                from i in t.GetInterfaces()
                where typeof (IHaveCustomMappings).IsAssignableFrom(t) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select (IHaveCustomMappings) Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IMapFrom<>) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select new
                {
                    Source = i.GetGenericArguments()[0],
                    Destination = t
                }).ToArray();

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadModelToEntityMappings()
        {
            Mapper.CreateMap<UserCreateModel, User>(MemberList.Source)
                .ForMember(s => s.Educations, t => t.Ignore())
                .ForMember(s => s.Experiences, t => t.Ignore())
                .ForMember(s => s.Projects, t => t.Ignore())
                .ForMember(s => s.Skills, t => t.Ignore());

            Mapper.CreateMap<SkillModel, Skill>(MemberList.Source);

            Mapper.CreateMap<ProjectModel, Project>(MemberList.Source);

            Mapper.CreateMap<ExperienceModel, Experience>(MemberList.Source);

            Mapper.CreateMap<EducationModel, Education>(MemberList.Source)
                .ForMember(s => s.Institution, t => t.Ignore());

            Mapper.CreateMap<ChoiceModel, Choice>();

            Mapper.CreateMap<QuestionCreateModel, Question>();
            Mapper.CreateMap<QuestionCategoryModel, QuestionCategory>();

            Mapper.CreateMap<QuizModel, Quiz>();
            Mapper.CreateMap<QuizPageCreateModel, QuizPage>();
            Mapper.CreateMap<QuizQuestionCreateModel, QuizQuestion>();
        }
    }
}
