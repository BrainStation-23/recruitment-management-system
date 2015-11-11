using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RecruitmentManagementSystem.App.ViewModels.Account
{
    public class AvatarViewModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Avatar { get; set; }
    }
}