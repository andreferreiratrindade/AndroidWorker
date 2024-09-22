
using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Domain.DTO
{
    public class ActivityDto
    {
        public Guid ActivityId {get;set;}
        public DateTime TimeActivityStart { get;  set;}
        public DateTime TimeActivityEnd { get;  set;}
        public DateTime TimeRestEnd{get;  set;}
        public List<string> WorkerId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get; set; }
        public TypeActivityStatus TypeActivityStatus {get;set;}
    }
}
