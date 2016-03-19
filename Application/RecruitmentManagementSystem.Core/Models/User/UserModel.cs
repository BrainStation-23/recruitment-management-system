using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Shared;

namespace RecruitmentManagementSystem.Core.Models.User
{
    public class UserModel : UserBaseModel, IMapFrom<Model.User>
    {
        public FileModel Avatar { get; set; }

        public FileModel Resume { get; set; }
    }
}