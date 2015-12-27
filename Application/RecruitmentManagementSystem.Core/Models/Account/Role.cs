using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Core.Models.Account
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
    }
}