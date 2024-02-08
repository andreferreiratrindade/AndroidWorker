using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class AddedRestRejectedEvent : DomainEvent
    {
        public Guid ActivityId { get;}
        public string WorkerId { get; }
        public Enums.TypeStatus Status { get; }
        public List<string> DescriptionErros { get; }

        public AddedRestRejectedEvent(string workerId,
                                      Enums.TypeStatus status,
                                      Guid activityId,
                                      List<string> descriptionErros)
        {
            this.Status = status;
            this.WorkerId = workerId;
            this.ActivityId = activityId;
            this.DescriptionErros = descriptionErros;
             CorrelationId = activityId;

        }
    }
}
