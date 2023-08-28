using Rests.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.DTOs
{
    public class RestDto
    {
        public RestDto()
        {
            
        }
        public Guid RestId { get; set; }
        public string WorkerId { get; set; }
        public DateTime TimeRestStart { get; set; }
        public DateTime TimeRestEnd { get; set; }
        public Guid ActivityId { get; set; }
        public TypeActivityBuild TypeActivityBuild { get; set; }
    }
}
