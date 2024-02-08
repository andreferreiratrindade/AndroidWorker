using Framework.Core.Data;
using ActivityValidationResult.Domain.Models.Entities;

namespace ActivityValidationResult.Domain.Models.Repositories
{
    public interface IActivityValidationResultRepository
    {
        Task Update(ActivityValidationResultEntity ActivityValidationResult);
        Task Add(ActivityValidationResultEntity ActivityValidationResult);
        void Delete(Guid ActivityValidationResultId);

        ActivityValidationResultEntity? GetById(Guid ActivityValidationResultId);

        Task<ActivityValidationResultEntity> GetByActivityId(Guid ActivityId);
    }
}