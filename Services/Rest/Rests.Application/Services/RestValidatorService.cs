using Rests.Domain.DTOs;
using Rests.Domain.Models.Repositories;
using Rests.Domain.ValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Rests.Application.Services
{

    public class RestValidatorService : IRestValidatorService
    {
        private readonly IRestRepository _restRepository;
        public RestValidatorService(IRestRepository restRepository)
        {
            _restRepository = restRepository;
        }


        public List<RestDto> GetWorkerInActivityByTimeActivityStartAndEnd(string workerId, DateTime timeActivityStart, DateTime timeRestEnd, Guid ignoreActivityId)
        {

            const string sql = @"SELECT rests.ActivityId,
                                        rests.WorkerId,
                                        rests.TimeRestStart,
                                        rests.TimeRestEnd
                               FROM Rests rests 
                              
                                where 
                                    rests.IsAlive = 1
                                    and rests.ActivityId <> @ignoreActivityId
                                    and 
                                    not(
                                        
                                        ( @timeActivityStart < rests.TimeRestStart and @timeRestEnd < rests.TimeRestStart)
                                        or
                                        ( @timeActivityStart > rests.TimeRestEnd and @timeActivityStart > rests.TimeRestEnd)
                                    ) 
                                    and rests.WorkerId = @workerId ";
            ;
            var lst = _restRepository.GetConnection().Query<RestDto>(sql, new { workerId, timeActivityStart, timeRestEnd, ignoreActivityId });
            return lst.ToList();
        }
    }
}
