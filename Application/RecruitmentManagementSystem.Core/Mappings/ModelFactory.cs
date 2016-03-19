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
        public TEntity MapToDomain<TModel, TEntity>(TModel model, TEntity entity)
            where TEntity : BaseEntity
            where TModel : BaseModel
        {
            var mappedEntity = Mapper.Map<TModel, TEntity>(model);

            if (entity != null)
            {
                mappedEntity.CreatedBy = entity.CreatedBy;
                mappedEntity.CreatedAt = entity.CreatedAt;
            }

            return MapObjectState(model, mappedEntity);
        }

        public ICollection<TEntity> MapToDomain<TModel, TEntity>(ICollection<TModel> models)
            where TEntity : BaseEntity
            where TModel : BaseModel
        {
            var mappedEntities = new List<TEntity>();

            if (models == null) return mappedEntities;

            mappedEntities.AddRange(from model in models
                let entity = Mapper.Map<TModel, TEntity>(model)
                select MapObjectState(model, entity));

            return mappedEntities;
        }

        private static TEntity MapObjectState<TModel, TEntity>(TModel model, TEntity entity)
            where TEntity : BaseEntity
            where TModel : BaseModel
        {
            if (model.Id == default(int))
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