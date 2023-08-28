using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.Domain.Enums;

namespace Activities.Domain.DTO
{
    public class WorkActiveReportDto
    {
        public WorkActiveReportDto()
        {
            Activities = new List<WorkActiviteActivityReportDto>();
        }
        public string WorkerId {get;set;}
       public List<WorkActiviteActivityReportDto> Activities {get;set;}
    }
}