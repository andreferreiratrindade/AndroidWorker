using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrchestration.Models
{
    public class ActivityStateInstance : SagaStateMachineInstance

    {
        public Guid CorrelationId { get; set; }

        public Guid ActivityId { get;  set; }
        public List<string> Workers { get;  set; }
        public int TypeActivityBuild { get;  set; }
        public DateTime TimeActivityStart { get;  set; }
        public DateTime TimeActivityEnd { get; set; }
    }
}
