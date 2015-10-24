using System.IO;
using System.Web;

namespace RecruitmentManagementSystem.App.Infrastructure.Helpers
{
    public class UploadConfig
    {
        public HttpPostedFileBase FileBase { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }

    public class FileHelper
    {
        public static void UploadFile(UploadConfig config)
        {
            if (string.IsNullOrEmpty(config.FileName))
            {
                config.FileName = config.FileBase.FileName;
            }

            var fullPath = HttpContext.Current.Server.MapPath(Path.Combine(config.FilePath, config.FileName));

            var dir = Path.GetDirectoryName(fullPath) ?? string.Empty;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            config.FileBase.SaveAs(fullPath);
        }
    }
}