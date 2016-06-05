using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RecruitmentManagementSystem.Core.Helpers;
using RecruitmentManagementSystem.Core.Infrastructure;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.User;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Services
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager;
        private readonly IModelFactory _modelFactory;
        private readonly ISkillRepository _skillRepository;
        private static IFileRepository _fileRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IEducationRepository _educationRepository;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { _userManager = value; }
        }

        public UserService(IEducationRepository educationRepository, IModelFactory modelFactory, IFileRepository fileRepository, ISkillRepository skillRepository,
            IProjectRepository projectRepository, IExperienceRepository experienceRepository)
        {
            _educationRepository = educationRepository;
            _modelFactory = modelFactory;
            _fileRepository = fileRepository;
            _skillRepository = skillRepository;
            _projectRepository = projectRepository;
            _experienceRepository = experienceRepository;
        }

        public void Update(UserCreateModel model)
        {
            var entity = UserManager.FindById(model.Id);

            entity.UserName = model.Email;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Address = model.Address;
            entity.Others = model.Others;
            entity.Website = model.Website;
            entity.Files = ManageFiles(model);

            #region Manage Educations

            foreach (var m in model.Educations.Where(m => string.IsNullOrEmpty(m.UserId)))
            {
                m.UserId = model.Id;
            }
            foreach (var education in model.Educations)
            {
                _educationRepository.InsertOrUpdate(_modelFactory.MapToDomain<EducationModel, Education>(education, null));
            }
            foreach (var m in entity.Educations.Where(y => model.Educations.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _educationRepository.Delete(m.Id);
            }

            _educationRepository.Save();

            #endregion

            #region Manage Experiences

            foreach (var m in model.Experiences.Where(m => string.IsNullOrEmpty(m.UserId)))
            {
                m.UserId = model.Id;
            }
            foreach (var m in model.Experiences)
            {
                _experienceRepository.InsertOrUpdate(_modelFactory.MapToDomain<ExperienceModel, Experience>(m, null));
            }
            foreach (var m in entity.Experiences.Where(y => model.Experiences.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _experienceRepository.Delete(m.Id);
            }

            _experienceRepository.Save();

            #endregion

            #region Manage Projects

            foreach (var m in model.Projects.Where(m => string.IsNullOrEmpty(m.UserId)))
            {
                m.UserId = model.Id;
            }
            foreach (var m in model.Projects)
            {
                _projectRepository.InsertOrUpdate(_modelFactory.MapToDomain<ProjectModel, Project>(m, null));
            }
            foreach (var m in entity.Projects.Where(y => model.Projects.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _projectRepository.Delete(m.Id);
            }

            _projectRepository.Save();

            #endregion

            #region Manage Skills

            foreach (var m in model.Skills.Where(m => string.IsNullOrEmpty(m.UserId)))
            {
                m.UserId = model.Id;
            }
            foreach (var m in model.Skills)
            {
                _skillRepository.InsertOrUpdate(_modelFactory.MapToDomain<SkillModel, Skill>(m, null));
            }
            foreach (var m in entity.Skills.Where(y => model.Skills.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _skillRepository.Delete(m.Id);
            }

            _skillRepository.Save();

            #endregion

            UserManager.Update(entity);
        }

        #region Private Methods

        private static ICollection<File> ManageFiles(UserCreateModel model)
        {
            var files = new List<File>();

            var httpFileCollection = HttpContext.Current.Request.Files;

            for (var index = 0; index < httpFileCollection.Count; index++)
            {
                if (httpFileCollection[index] == null || httpFileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                FileType fileType;
                if (httpFileCollection[index].FileName == model.AvatarFileName)
                {
                    fileType = FileType.Avatar;
                }
                else if (httpFileCollection[index].FileName == model.ResumeFileName)
                {
                    fileType = FileType.Resume;
                }
                else
                {
                    fileType = FileType.Document;
                }

                var uploadConfig = FileHelper.Upload(httpFileCollection[index], fileType);

                if (uploadConfig.FileBase == null) continue;

                var existingRecords = _fileRepository.FindAll(x => x.User.Id == model.Id).Select(x => new
                {
                    x.Id,
                    x.RelativePath
                }).ToList();
                foreach (var record in existingRecords)
                {
                    FileHelper.Delete(record.RelativePath);
                    _fileRepository.Delete(record.Id);
                }
                _fileRepository.Save();

                var file = new File
                {
                    Name = uploadConfig.FileName,
                    MimeType = uploadConfig.FileBase.ContentType,
                    Size = uploadConfig.FileBase.ContentLength,
                    RelativePath = uploadConfig.FilePath + uploadConfig.FileName,
                    FileType = fileType,
                    ObjectState = ObjectState.Added,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                    UpdatedBy = HttpContext.Current.User.Identity.GetUserId()
                };

                files.Add(file);
            }

            return files;
        }

        #endregion
    }
}