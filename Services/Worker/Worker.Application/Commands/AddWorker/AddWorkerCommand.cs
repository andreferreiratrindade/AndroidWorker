using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using MassTransit;
using System.Text.Json.Serialization;
using Worker.Domain.DomainEvents;
using Framework.Core.DomainObjects;

namespace Worker.Application.Commands.AddWorker
{
    public class AddWorkerCommand : Command<AddWorkerCommandOutput>
    {

        [Required]
        public List<string> Workers { get; set; }

        public AddWorkerCommand(List<string> workers, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.Workers = workers;

            this.AddValidCommand(new AddWorkerCommandValidation().Validate(this));

            this.AddRollBackEvent(new WorkerAddCompensationEvent(this.Workers,correlationId));
        }
    }
}
