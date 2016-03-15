using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class UserCreateModel : BaseModel
    {
        public PersonalInformation PersonalInformation { get; set; }

        public SecurityModel SecurityModel { get; set; }
    }
}