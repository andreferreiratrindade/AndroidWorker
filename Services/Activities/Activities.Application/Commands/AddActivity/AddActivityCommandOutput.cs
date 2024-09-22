using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Application.Commands.AddActivity
{
    public class AddActivityCommandOutput
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get;set; }
        public DateTime TimeActivityStart { get; set; }
        public DateTime TimeActivityEnd { get;set; }
        public List<string> Workers{get;set; }
    }
}
