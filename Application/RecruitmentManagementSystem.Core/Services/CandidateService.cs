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

        public IEnumerable<CandidateDto> GetPagedList()
        {
            var model = _candidateRepository.FindAll().ProjectTo<CandidateDto>();

            return model;
        }

        public void Insert(CandidateCreateDto model)
        {
            var entity = _modelFactory.MapToDomain<CandidateCreateDto, Candidate>(model, null);

            entity.Educations = _modelFactory.MapToDomain<EducationDto, Education>(model.Educations);
            entity.Experiences = _modelFactory.MapToDomain<ExperienceDto, Experience>(model.Experiences);
            entity.Projects = _modelFactory.MapToDomain<ProjectDto, Project>(model.Projects);
            entity.Skills = _modelFactory.MapToDomain<SkillDto, Skill>(model.Skills);

            entity.Files = ManageFiles(model);

            _candidateRepository.Insert(entity);
            _candidateRepository.Save();
        }

        public void Update(CandidateCreateDto model)
        {
            var entity = _candidateRepository.FindIncluding(x => x.Id == model.Id, x => x.Educations, x => x.Experiences, x => x.Projects, x => x.Skills);

            if (entity == null) return;

            foreach (var dto in model.Educations.Where(dto => dto.CandidateId == default(int)))
            {
                dto.CandidateId = model.Id;
            }
            foreach (var dto in model.Experiences.Where(dto => dto.CandidateId == default(int)))
            {
                dto.CandidateId = model.Id;
            }
            foreach (var dto in model.Projects.Where(dto => dto.CandidateId == default(int)))
            {
                dto.CandidateId = model.Id;
            }
            foreach (var dto in model.Skills.Where(dto => dto.CandidateId == default(int)))
            {
                dto.CandidateId = model.Id;
            }

            var updatedEntity = _modelFactory.MapToDomain(model, entity);

            updatedEntity.Educations = _modelFactory.MapToDomain<EducationDto, Education>(model.Educations);
            updatedEntity.Experiences = _modelFactory.MapToDomain<ExperienceDto, Experience>(model.Experiences);
            updatedEntity.Projects = _modelFactory.MapToDomain<ProjectDto, Project>(model.Projects);
            updatedEntity.Skills = _modelFactory.MapToDomain<SkillDto, Skill>(model.Skills);
            updatedEntity.Files = ManageFiles(model);

            _candidateRepository.Update(updatedEntity);

            foreach (var x in entity.Educations.Where(y => model.Educations.FirstOrDefault(z => z.Id == y.Id) == null))
            {
                _educationRepository.Delete(x.Id);
            }
            foreach (var x in entity.Experiences.Where(y => model.Experiences.FirstOrDefault(z => z.Id == y.Id) == null))
            {
                _experienceRepository.Delete(x.Id);
            }
            foreach (var x in entity.Projects.Where(y => model.Projects.FirstOrDefault(z => z.Id == y.Id) == null))
            {
                _projectRepository.Delete(x.Id);
            }
            foreach (var x in entity.Skills.Where(y => model.Skills.FirstOrDefault(z => z.Id == y.Id) == null))
            {
                _skillRepository.Delete(x.Id);
            }

            _candidateRepository.Save();
            _educationRepository.Save();
            _experienceRepository.Save();
            _projectRepository.Save();
            _skillRepository.Save();
        }

        #region Private Methods

        private static ICollection<File> ManageFiles(CandidateBase dto)
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
                if (fileCollection[index].FileName == dto.AvatarFileName)
                {
                    fileType = FileType.Avatar;
                }
                else if (fileCollection[index].FileName == dto.ResumeFileName)
                {
                    fileType = FileType.Resume;
                }
                else
                {
                    fileType = FileType.Document;
                }

                var uploadConfig = FileHelper.Upload(fileCollection[index], fileType);

                if (uploadConfig.FileBase == null) continue;

                var existingRecords = _fileRepository.FindAll(x => x.Candidate.Id == dto.Id).Select(x => new
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