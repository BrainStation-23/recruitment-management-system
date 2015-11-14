using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.App.ViewModels.Account
{
    public class RoleViewModel
    {
        [Required]
        public string Role { get; set; }
    }
}