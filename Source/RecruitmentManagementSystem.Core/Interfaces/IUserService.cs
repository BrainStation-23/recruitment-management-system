using RecruitmentManagementSystem.Core.Models.User;

namespace RecruitmentManagementSystem.Core.Interfaces
{
    public interface IUserService
    {
        void Update(UserCreateModel model);
    }
}