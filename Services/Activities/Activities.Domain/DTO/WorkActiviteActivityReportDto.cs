

using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Domain.DTO
{
    public class WorkActiviteActivityReportDto
    {
        public Guid ActivityId {get;set;}
        public DateTime TimeActivityStart { get;  set;}
        public DateTime TimeActivityEnd { get;  set;}
        public DateTime TimeRestEnd{get;  set;}
        public TypeActivityBuild TypeActivityBuild { get; set; }
    }
}
