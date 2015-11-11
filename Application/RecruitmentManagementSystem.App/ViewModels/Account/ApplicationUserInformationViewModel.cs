using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.App.ViewModels.Account
{
    public class ApplicationUserInformationViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}