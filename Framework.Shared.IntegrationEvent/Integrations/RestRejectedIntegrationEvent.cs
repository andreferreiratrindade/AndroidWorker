using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("RestRejected", ExchangeName = "RestExchange")]

    public class RestRejectedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public List<string> DescriptionErros { get; }
       public Guid ActivityId{get;}
        public string WorkerId { get; }
       public RestRejectedIntegrationEvent(Guid activityId, string workerId, List<string> descriptionErros, CorrelationIdGuid correlationId): base(correlationId)
        {
            this.WorkerId = workerId;
            ActivityId = activityId;
            DescriptionErros = descriptionErros;
        }
    }
}
