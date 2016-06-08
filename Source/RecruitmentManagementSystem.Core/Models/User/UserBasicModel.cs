using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class UserBasicModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        [Display(Name = "Avatar")]
        public FileModel Avatar { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Model.User, UserBasicModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}