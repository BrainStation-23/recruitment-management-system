using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.Core.Helpers;
using RecruitmentManagementSystem.Core.Interfaces;
using RecruitmentManagementSystem.Core.Mappings;
using RecruitmentManagementSystem.Core.Models.Candidate;
using RecruitmentManagementSystem.Data.Interfaces;
using RecruitmentManagementSystem.Model;
using File = RecruitmentManagementSystem.Model.File;

namespace RecruitmentManagementSystem.Core.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IModelFactory _modelFactory;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;
        private static IFileRepository _fileRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IEducationRepository _educationRepository;

        public CandidateService(ICandidateRepository candidateRepository,
            IModelFactory modelFactory,
            IFileRepository fileRepository,
            ISkillRepository skillRepository,
            IProjectRepository projectRepository,
            IExperienceRepository experienceRepository,
            IEducationRepository educationRepository)
        {
            _candidateRepository = candidateRepository;
            _modelFactory = modelFactory;
            _fileRepository = fileRepository;
            _skillRepository = skillRepository;
            _projectRepository = projectRepository;
            _experienceRepository = experienceRepository;
            _educationRepository = educationRepository;
        }

        public IEnumerable<CandidateModel> GetPagedList()
        {
            var model = _candidateRepository.FindAll().ProjectTo<CandidateModel>();

            return model;
        }

        public void Insert(CandidateCreateModel model)
        {
            var entity = _modelFactory.MapToDomain<CandidateCreateModel, Candidate>(model, null);

            entity.Educations = _modelFactory.MapToDomain<EducationModel, Education>(model.Educations);
            entity.Experiences = _modelFactory.MapToDomain<ExperienceModel, Experience>(model.Experiences);
            entity.Projects = _modelFactory.MapToDomain<ProjectModel, Project>(model.Projects);
            entity.Skills = _modelFactory.MapToDomain<SkillModel, Skill>(model.Skills);

            entity.Files = ManageFiles(model);

            _candidateRepository.Insert(entity);
            _candidateRepository.Save();
        }

        public void Update(CandidateCreateModel model)
        {
            var entity = _candidateRepository.FindIncluding(x => x.Id == model.Id, x => x.Educations, x => x.Experiences, x => x.Projects, x => x.Skills);

            if (entity == null) return;

            foreach (var m in model.Educations.Where(x => x.CandidateId == default(int)))
            {
                m.CandidateId = model.Id;
            }
            foreach (var m in model.Experiences.Where(x => x.CandidateId == default(int)))
            {
                m.CandidateId = model.Id;
            }
            foreach (var m in model.Projects.Where(x => x.CandidateId == default(int)))
            {
                m.CandidateId = model.Id;
            }
            foreach (var m in model.Skills.Where(x => x.CandidateId == default(int)))
            {
                m.CandidateId = model.Id;
            }

            var updatedEntity = _modelFactory.MapToDomain(model, entity);

            updatedEntity.Educations = _modelFactory.MapToDomain<EducationModel, Education>(model.Educations);
            updatedEntity.Experiences = _modelFactory.MapToDomain<ExperienceModel, Experience>(model.Experiences);
            updatedEntity.Projects = _modelFactory.MapToDomain<ProjectModel, Project>(model.Projects);
            updatedEntity.Skills = _modelFactory.MapToDomain<SkillModel, Skill>(model.Skills);
            updatedEntity.Files = ManageFiles(model);

            _candidateRepository.Update(updatedEntity);

            foreach (var m in entity.Educations.Where(y => model.Educations.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _educationRepository.Delete(m.Id);
            }
            foreach (var m in entity.Experiences.Where(y => model.Experiences.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _experienceRepository.Delete(m.Id);
            }
            foreach (var m in entity.Projects.Where(y => model.Projects.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _projectRepository.Delete(m.Id);
            }
            foreach (var m in entity.Skills.Where(y => model.Skills.FirstOrDefault(x => x.Id == y.Id) == null))
            {
                _skillRepository.Delete(m.Id);
            }

            _candidateRepository.Save();
            _educationRepository.Save();
            _experienceRepository.Save();
            _projectRepository.Save();
            _skillRepository.Save();
        }

        #region Private Methods

        private static ICollection<File> ManageFiles(CandidateBase model)
        {
            var files = new List<File>();
            var fileCollection = HttpContext.Current.Request.Files;

            for (var index = 0; index < fileCollection.Count; index++)
            {
                if (fileCollection[index].ContentLength <= 0)
                {
                    continue;
                }

                FileType fileType;
                if (fileCollection[index].FileName == model.AvatarFileName)
                {
                    fileType = FileType.Avatar;
                }
                else if (fileCollection[index].FileName == model.ResumeFileName)
                {
                    fileType = FileType.Resume;
                }
                else
                {
                    fileType = FileType.Document;
                }

                var uploadConfig = FileHelper.Upload(fileCollection[index], fileType);

                if (uploadConfig.FileBase == null) continue;

                var existingRecords = _fileRepository.FindAll(x => x.Candidate.Id == model.Id).Select(x => new
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