using ActivityValidationResult.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DTOs
{
    public class ActivityValidationResultDto
    {
        public ActivityValidationResultDto()
        {
            
        }
        public Guid ActivityValidationResultId { get; set; }
        public string WorkerId { get; set; }
        public DateTime TimeActivityValidationResulttart { get; set; }
        public DateTime TimeActivityValidationResultEnd { get; set; }
        public Guid ActivityId { get; set; }
        public TypeStatus TypeActivityBuild { get; set; }
    }
}
