using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Core.Models.Account
{
    public class AccountDetails
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}