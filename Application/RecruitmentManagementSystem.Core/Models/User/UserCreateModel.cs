using RecruitmentManagementSystem.Core.Mappings;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class UserCreateModel: UserBaseModel, IMapFrom<Model.User>
    {
        public string AvatarFileName { get; set; }

        public string ResumeFileName { get; set; }
    }
}