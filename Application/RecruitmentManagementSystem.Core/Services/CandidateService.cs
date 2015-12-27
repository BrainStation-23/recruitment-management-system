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
        private static IFileRepository _fileRepository;

        public CandidateService(ICandidateRepository candidateRepository,
            IModelFactory modelFactory, IFileRepository fileRepository)
        {
            _candidateRepository = candidateRepository;
            _modelFactory = modelFactory;
            _fileRepository = fileRepository;
        }

        public IEnumerable<CandidateDto> GetPagedList()
        {
            var model = _candidateRepository.FindAll().ProjectTo<CandidateDto>();

            return model;
        }

        public void Insert(CandidateCreateDto model)
        {
            var entity = _modelFactory.MapToDomain<CandidateCreateDto, Candidate>(model, null);

            entity.Educations = _modelFactory.MapToDomain<EducationDto, Education>(model.Educations, null);
            entity.Experiences = _modelFactory.MapToDomain<ExperienceDto, Experience>(model.Experiences, null);
            entity.Projects = _modelFactory.MapToDomain<ProjectDto, Project>(model.Projects, null);
            entity.Skills = _modelFactory.MapToDomain<SkillDto, Skill>(model.Skills, null);

            entity.Files = ManageFiles(model);

            _candidateRepository.Insert(entity);
            _candidateRepository.Save();
        }

        public void Update(CandidateCreateDto model)
        {
            var entity = _candidateRepository.FindIncluding(x => x.Id == model.Id, x => x.Educations,
                x => x.Experiences, x => x.Projects, x => x.Skills);

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

            updatedEntity.Educations = _modelFactory.MapToDomain<EducationDto, Education>(model.Educations,
                entity.Educations);
            updatedEntity.Experiences = _modelFactory.MapToDomain<ExperienceDto, Experience>(model.Experiences,
                entity.Experiences);
            updatedEntity.Projects = _modelFactory.MapToDomain<ProjectDto, Project>(model.Projects, entity.Projects);
            updatedEntity.Skills = _modelFactory.MapToDomain<SkillDto, Skill>(model.Skills, entity.Skills);

            updatedEntity.Files = ManageFiles(model);

            _candidateRepository.Update(updatedEntity);
            _candidateRepository.Save();
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