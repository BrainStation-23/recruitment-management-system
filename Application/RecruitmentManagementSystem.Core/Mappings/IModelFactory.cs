using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Mappings
{
    public interface IModelFactory
    {
        ICollection<TEntity> MapToDomain<TModel, TEntity>(ICollection<TModel> models)
            where TEntity : BaseEntity
            where TModel : BaseModel;

        TEntity MapToDomain<TModel, TEntity>(TModel model, TEntity entity)
            where TEntity : BaseEntity
            where TModel : BaseModel;
    }
}