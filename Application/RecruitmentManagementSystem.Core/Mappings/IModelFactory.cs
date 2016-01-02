using System.Collections.Generic;
using RecruitmentManagementSystem.Core.Models.Shared;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Core.Mappings
{
    public interface IModelFactory
    {
        ICollection<TEntity> MapToDomain<TDto, TEntity>(ICollection<TDto> models)
            where TEntity : BaseEntity
            where TDto : BaseDto;

        TEntity MapToDomain<TDto, TEntity>(TDto dto, TEntity entity)
            where TEntity : BaseEntity
            where TDto : BaseDto;
    }
}