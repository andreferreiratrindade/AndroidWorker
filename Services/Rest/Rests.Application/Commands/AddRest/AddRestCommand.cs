
using FluentValidation.Results;
using Framework.Core.Messages;
using Rests.Domain.Enums;

namespace Rests.Application.Commands.AddRest
{
    public class AddRestCommand : Command<AddRestCommandOutput>
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get; set; }
        public DateTime TimeRestStart { get; set; }
        public DateTime TimeActivityStart { get; set; }
        public List<string> Workers { get; set;}


        public AddRestCommand(Guid activityId,  TypeActivityBuild typeActivityBuild, DateTime timeActivityStart, DateTime timeRestStart, List<string> workers)
        {   
            this.ActivityId = activityId;
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeRestStart = timeRestStart;
            this.TimeActivityStart = timeActivityStart;
            this.Workers = workers;

            // ValidCommand(new AddActivityCommandValidation().Validate(this));
        }
    }
}