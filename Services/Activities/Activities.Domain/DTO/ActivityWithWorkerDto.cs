using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.DTO
{
    public class ActivityWithWorkerDto
    {
        public Guid ActivityId {get;set;}
        public DateTime TimeActivityStart { get;  set;}
        public DateTime TimeActivityEnd { get;  set;}
        public DateTime TimeRestEnd{get;  set;}
        public string WorkerId {get;set;}
    }
}