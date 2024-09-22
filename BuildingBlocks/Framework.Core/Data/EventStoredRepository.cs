using Framework.Core.Messages;
using Framework.Core.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Framework.Core.Data
{
    public class EventStoredRepository : IEventStoredRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;

        public EventStoredRepository(IOptions<MongoDbConfig> config)
        {
            var mongoClient = new MongoClient(config.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

            _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
        }

        public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
        {
            try
            {
                //var tt = await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync();
                var tt2 = await _eventStoreCollection.FindAsync(x => x.AggregateIdentifier == aggregateId);

                return tt2.ToList();
            }
            catch (Exception ex)
            {


            }

            return null;
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
