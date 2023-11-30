using Framework.Message.Bus.Queues;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrchestration.Models
{
    public class ActivityStateMachine : MassTransitStateMachine<ActivityStateInstance>
    {
        public Event<ActivityCreatedIntegrationEvent> ActivityCreatedIntegrationEvent { get; set; }
        public Event<ActivityNotCreatedIntegrationEvent> ActivityNotCreatedIntegrationEvent { get; set; }
        public Event<RestAddedIntegrationEvent> RestAddedIntegrationEvent { get; set; }
        public Event<RestNotAddedIntegrationEvent> RestNotAddedIntegrationEvent { get; set; }
        public State ActivityCreated { get; private set; }
        public State ActivityNotCreated { get; private set; }
        public State RestAdded { get; private set; }
        public State RestNotAdded { get; private set; }

        public ActivityStateMachine()
        {
            //InstanceState(x=> x.Current)
            Event(() => ActivityCreatedIntegrationEvent,
                    y => y.CorrelateBy<Guid>(x => x.ActivityId, e => e.Message.ActivityId)
                        .SelectId(context => Guid.NewGuid()));
            Event(() => ActivityNotCreatedIntegrationEvent, y => y.CorrelateById(x => x.Message.CorrelationById));
            Event(() => RestAddedIntegrationEvent, y => y.CorrelateById(x => x.Message.CorrelationById));
            Event(() => RestNotAddedIntegrationEvent, y => y.CorrelateById(x => x.Message.CorrelationById));


            Initially(When(ActivityCreatedIntegrationEvent)
              .Then(context =>
              {
                  context.Instance.ActivityId = context.Data.ActivityId;
                  context.Instance.TimeActivityStart = context.Data.TimeActivityStart;
                  context.Instance.TimeActivityEnd = context.Data.TimeActivityEnd;
                  context.Instance.TypeActivityBuild = context.Data.TypeActivityBuild;
                  context.Instance.Workers = context.Data.Workers;
              })
              .Then(context => { Console.WriteLine($"ActivityCreatedIntegrationEvent before: {context.Instance}"); })
              .TransitionTo(ActivityCreated)
              .Publish(context => new ActivityCreatedIntegrationEvent(context.Data.ActivityId,
                                                                      context.Data.Workers,
                                                                      context.Data.TypeActivityBuild,
                                                                      context.Data.TimeActivityStart,
                                                                      context.Data.TimeActivityEnd,
                                                                      context.Instance.CorrelationId))
               .Then(context => { Console.WriteLine($"ActivityCreatedIntegrationEvent after: {context.Instance}"); }));


            During(ActivityCreated, 
                When(RestAddedIntegrationEvent)
                    .TransitionTo(RestAdded)
                    .Send(new Uri($"queue:{RabbitMqQueue.RestAddedRequestQueueName}"), context => 
                        new RestAddedRequestIntegrationEvent(context.Instance.ActivityId,
                                                             context.Instance.Workers,
                                                             context.Instance.TypeActivityBuild,
                                                             context.Instance.TimeActivityStart,
                                                             context.Instance.TimeActivityEnd,
                                                             context.Instance.CorrelationId))
                   .Then(context => { Console.WriteLine($"Rest added after: {context.Instance}"); }),
                When(RestNotAddedIntegrationEvent)
                    .TransitionTo(RestNotAdded)
                    .Publish(context=> new ActivityRequestFailedEventQueueName)




        }
    }
}
