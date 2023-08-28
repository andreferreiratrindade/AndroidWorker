using Framework.Core.Data;
using Rests.Domain.Models.Entities;

namespace Rests.Domain.Models.Repositories
{
    public interface IRestRepository : IRepository<Rest>
    {
        void Update(Rest rest);
        void Add(Rest rest);
        void Delete(Guid restId);

        Rest? GetById(Guid restId);
        IList<Rest> GetByActivityId(Guid activityId);
        Task<Rest?> GetByIdAsync(Guid restId);

        IQueryable<Rest> GetQueryable();
    }
}