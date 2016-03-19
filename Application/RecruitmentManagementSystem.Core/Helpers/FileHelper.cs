using System;
using System.IO;
using System.Web;
using RecruitmentManagementSystem.Core.Constants;
using RecruitmentManagementSystem.Model;

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
        public static UploadConfig Upload(HttpPostedFile fileBase, FileType fileType)
        {
            //var fileName = $"{Guid.NewGuid()}.{Path.GetFileName(fileBase.FileName)}";
            var fileName = String.Format("{0}{1}",Guid.NewGuid(),Path.GetFileName(fileBase.FileName));

            var filePath = string.Empty;

            switch (fileType)
            {
                case FileType.Avatar:
                    filePath = FilePath.AvatarRelativePath;
                    break;
                case FileType.Resume:
                    filePath = FilePath.ResumeRelativePath;
                    break;
                case FileType.Document:
                    filePath = FilePath.DocumentRelativePath;
                    break;
            }

            var uploadConfig = new UploadConfig
            {
                FileBase = fileBase,
                FileName = fileName,
                FilePath = filePath
            };

            try
            {
                SaveAs(uploadConfig);
            }
            catch (Exception)
            {
                return new UploadConfig();
            }

            return uploadConfig;
        }

        public static void Delete(string relativePath)
        {
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(relativePath)))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(relativePath));
            }
        }

        private static void SaveAs(UploadConfig config)
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
    }
}