using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Framework.Core.EventSourcingUtils;
using System.Xml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Framework.Core.Data
{
    public class EventStored : IEventStored
    {

        private readonly IEventStoredRepository _eventStoreRepository;
        public EventStored(IEventStoredRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;

        }


        public async Task SaveAsync(IEnumerable<IDomainEvent> events, Guid aggregateId, string aggregateType)
        {
            var version = 0;

            foreach (var @event in events)
            {
                version++;
                var eventModel = new EventModel
                {
                    TimeStamp = @event.TimeStamp,
                    AggregateIdentifier = aggregateId,
                    AggregateType = aggregateType,
                    Version = version,
                    EventType = @event.GetType().Name,
                    EventData = JsonConvert.SerializeObject(@event)
                };

                await _eventStoreRepository.SaveAsync(eventModel);
            }
        }


        public Task<T> Get<T>(Guid aggregateId, CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            return LoadAggregate<T>(aggregateId, cancellationToken);
        }

        private async Task<T> LoadAggregate<T>(Guid aggregateId, CancellationToken cancellationToken = default) where T : AggregateRoot
        {
            var persistedEvents = new List<IDomainEvent>();
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if (eventStream == null || !eventStream.Any())
                throw new Exception("Incorrect Id");

            var aggregate = AggregateFactory<T>.CreateAggregate();
            var lstEvents = new List<IDomainEvent>();
            foreach (var @event in eventStream)
            {

                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(@event.EventData);
                Console.Write(domainEvent);
            }
            aggregate.LoadFromHistory(0, lstEvents);
            return aggregate;
        }
    }
}
