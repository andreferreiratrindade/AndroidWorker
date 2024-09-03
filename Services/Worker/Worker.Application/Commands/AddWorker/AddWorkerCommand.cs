using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using MassTransit;
using System.Text.Json.Serialization;

namespace Worker.Application.Commands.AddWorker
{
    public class AddWorkerCommand : Command<AddWorkerCommandOutput>, CorrelatedBy<Guid>
    {

        [Required]
        public List<string> Workers { get; set; }

        [JsonIgnore]
        public Guid CorrelationId { get; set; }

        public AddWorkerCommand(List<string> workers)
        {
            Workers = Workers;

            this.AddValidCommand(new AddWorkerCommandValidation().Validate(this));
            this.AddCommandOutput(new AddWorkerCommandOutput());
        }
    }
}
