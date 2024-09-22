using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Application.Commands.RejectActivity
{
    public class RejectActivityCommandOutput
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get;set; }
        public DateTime TimeActivityStart { get; set; }
        public DateTime TimeActivityEnd { get;set; }
        public DateTime TimeRestEnd { get;set; }
    }
}
