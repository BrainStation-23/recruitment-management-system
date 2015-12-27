using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using RecruitmentManagementSystem.Core.Models.Candidate;
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

            LoadDtoToDomainMappings();
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

        private static void LoadDtoToDomainMappings()
        {
            Mapper.CreateMap<CandidateCreateDto, Candidate>(MemberList.Source)
                .ForMember(s => s.Educations, t => t.Ignore())
                .ForMember(s => s.Experiences, t => t.Ignore())
                .ForMember(s => s.Projects, t => t.Ignore())
                .ForMember(s => s.Skills, t => t.Ignore());

            Mapper.CreateMap<SkillDto, Skill>(MemberList.Source);

            Mapper.CreateMap<ProjectDto, Project>(MemberList.Source);

            Mapper.CreateMap<ExperienceDto, Experience>(MemberList.Source);

            Mapper.CreateMap<EducationDto, Education>(MemberList.Source)
                .ForMember(s => s.Institution, t => t.Ignore())
                .ForMember(s => s.Candidate, t => t.Ignore());
        }
    }
}