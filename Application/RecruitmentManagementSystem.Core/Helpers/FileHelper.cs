using System;
using System.IO;
using System.Web;
using RecruitmentManagementSystem.Core.Constants;
using File = RecruitmentManagementSystem.Model.File;

namespace RecruitmentManagementSystem.Core.Helpers
{
    public class UploadConfig
    {
        public HttpPostedFile FileBase { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }

    public class FileHelper
    {
        public static void SaveFile(UploadConfig config)
        {
            if (config.FileBase == null || config.FileBase.ContentLength <= 0)
            {
                throw new FileNotFoundException("File not found.");
            }

            if (string.IsNullOrEmpty(config.FileName))
            {
                config.FileName = config.FileBase.FileName;
            }

            if (string.IsNullOrEmpty(config.FilePath))
            {
                config.FilePath = FilePath.DefaultRelativePath;
            }

            var fullPath = HttpContext.Current.Server.MapPath(Path.Combine(config.FilePath, config.FileName));

            var dir = Path.GetDirectoryName(fullPath) ?? string.Empty;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            config.FileBase.SaveAs(fullPath);
        }

        public static void DeleteFile(File file)
        {
            var fullPath = HttpContext.Current.Server.MapPath(file.RelativePath);

            try
            {
                System.IO.File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}