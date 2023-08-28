using Rests.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.ValidationServices
{
    public interface IRestValidatorService
    {
        List<RestDto> GetWorkerInActivityByTimeActivityStartAndEnd(string workerId, DateTime timeActivityStart, DateTime timeRestEnd, Guid ignoreActivityId);

    }
}
