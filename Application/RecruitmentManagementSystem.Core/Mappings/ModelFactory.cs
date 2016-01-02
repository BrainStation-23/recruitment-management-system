using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Mappings
{
    public class ModelFactory : IModelFactory
    {
        public TEntity MapToDomain<TDto, TEntity>(TDto dto, TEntity entity)
            where TEntity : BaseEntity
            where TDto : BaseDto
        {
            var mappedEntity = Mapper.Map<TDto, TEntity>(dto);

            if (entity != null)
            {
                mappedEntity.CreatedBy = entity.CreatedBy;
                mappedEntity.CreatedAt = entity.CreatedAt;
            }

            return MapObjectState(dto, mappedEntity);
        }

        public ICollection<TEntity> MapToDomain<TDto, TEntity>(ICollection<TDto> models)
            where TEntity : BaseEntity
            where TDto : BaseDto
        {
            var mappedEntities = new List<TEntity>();

            if (models == null) return mappedEntities;

            mappedEntities.AddRange(from dto in models
                let entity = Mapper.Map<TDto, TEntity>(dto)
                select MapObjectState(dto, entity));

            return mappedEntities;
        }

        private static TEntity MapObjectState<TDto, TEntity>(TDto dto, TEntity entity)
            where TEntity : BaseEntity
            where TDto : BaseDto
        {
            if (dto.Id == default(int))
            {
                entity.ObjectState = ObjectState.Added;
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            }
            else
            {
                entity.ObjectState = ObjectState.Modified;
            }
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = HttpContext.Current.User.Identity.GetUserId();

            return entity;
        }
    }
}