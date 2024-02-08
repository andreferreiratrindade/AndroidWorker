using System.Data.Common;
using ActivityValidationResult.Domain.Models.Entities;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using ActivityValidationResult.Domain.Models.Repositories;
using Framework.Core.Messages;
using Framework.Core.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ActivityValidationResult.Infra.Data.Repository
{
    public class ActivityValidationResultRepository : IActivityValidationResultRepository
    {
        private readonly IMongoCollection<ActivityValidationResultEntity> _collection;


        public ActivityValidationResultRepository(IOptions<MongoDbConfig> mongoOptions, IMongoClient mongoClient)
        {
            //var mongoClient = new MongoClient(config.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

            //_collection = mongoDatabase.GetCollection<ActivityValidationResultEntity>(mongoOptions.Value.Collection);

            var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
            _collection = database.GetCollection<ActivityValidationResultEntity>(mongoOptions.Value.Collection);


        }



        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Add(ActivityValidationResultEntity entity)
        {
          await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        }

        public void Delete(Guid ActivityValidationResultId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           
        }

        public async Task<List<ActivityValidationResultEntity>> FindByAggregateId(Guid aggregateId)
        {
            try
            {
                //var tt = await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync();
                var tt2 = await _collection.FindAsync(x => x.Id == aggregateId.ToString());

                return tt2.ToList();
            }
            catch (Exception ex)
            {


            }

            return null;
        }

        public async Task<ActivityValidationResultEntity> GetByActivityId(Guid ActivityId)
        {
            var result =  await _collection.FindAsync(x=> x.ActivityId == ActivityId );
            return result.First();
        }

        public ActivityValidationResultEntity? GetById(Guid ActivityValidationResultId)
        {
            throw new NotImplementedException();
        }

        public DbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(ActivityValidationResultEntity model)
        {
            await _collection.InsertOneAsync(model).ConfigureAwait(false);
        }

        public async Task Update(ActivityValidationResultEntity activityValidationResult)
        {
          var filter = Builders<ActivityValidationResultEntity>.Filter.Eq(s => s.ActivityId, activityValidationResult.ActivityId);
           await _collection.ReplaceOneAsync(filter, activityValidationResult);
        }
    }
}