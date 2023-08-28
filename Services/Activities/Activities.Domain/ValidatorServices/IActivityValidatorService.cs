using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.Domain.DTO;

namespace Activities.Domain.ValidatorServices
{
    public interface IActivityValidatorService
    {
       List<ActivityWithWorkerDto> GetWorkersInActivityByTimeActivityStartAndEnd(List<string> workers, DateTime timeActivityStart, DateTime timeActivityEnd, Guid ignoreActivityId);
    }
}