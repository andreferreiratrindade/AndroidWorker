using System.Data.Common;
using Worker.Domain.Models.Entities;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Worker.Domain.Models.Repositories;

namespace Worker.Infra.Data.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly List<Worker.Domain.Models.Entities.Worker> _worker = new List<Worker.Domain.Models.Entities.Worker>(){
                new Worker.Domain.Models.Entities.Worker("A"),
                new Worker.Domain.Models.Entities.Worker("B")};

        public WorkerRepository()
        {
        }

        public IQueryable<Worker.Domain.Models.Entities.Worker> GetQueryable()
        {
            return _worker.AsQueryable();
        }

    }

}