using Framework.Core.Data;
using Worker.Domain.Models.Entities;

namespace Worker.Domain.Models.Repositories
{
    public interface IWorkerRepository
    {
        IQueryable<WorkerEntity> GetQueryable();
        Task Add(WorkerEntity entity);
    }
}
