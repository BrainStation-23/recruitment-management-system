using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RecruitmentManagementSystem.App.ViewModels.Common
{
    public class FileUploadViewModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}