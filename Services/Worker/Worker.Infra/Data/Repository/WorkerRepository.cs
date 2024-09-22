using System.Data.Common;
using Framework.Core.Data;

using Framework.Core.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Worker.Domain.Models.Entities;
using Worker.Domain.Models.Repositories;

namespace Worker.Infra.Data.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IMongoCollection<WorkerEntity> _collection;


        public WorkerRepository(IOptions<MongoDbConfig> mongoOptions, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
            _collection = database.GetCollection<WorkerEntity>(mongoOptions.Value.Collection);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Add(WorkerEntity entity)
        {
          await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        }


        public IQueryable<WorkerEntity> GetQueryable()
        {
           return _collection.AsQueryable();
        }

    }
}
