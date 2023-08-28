using Framework.Core.Data;
using Worker.Domain.Models.Entities;

namespace Worker.Domain.Models.Repositories
{
    public interface IWorkerRepository 
    {

        IQueryable<Worker.Domain.Models.Entities.Worker> GetQueryable();
    }
}