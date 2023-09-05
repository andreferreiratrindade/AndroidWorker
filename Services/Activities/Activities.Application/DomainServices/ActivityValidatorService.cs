using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Activities.Domain.ValidatorServices;
using Dapper;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DTO;

namespace Activities.Application.DomainServices
{
    public class ActivityValidatorService : IActivityValidatorService
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityValidatorService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }


        public List<ActivityWithWorkerDto> GetWorkersInActivityByTimeActivityStartAndEnd(List<string> workers, DateTime timeActivityStart, DateTime timeActivityEnd, Guid ignoreActivityId)
        {

            const string sql = @"SELECT ActivityId = activities.Id,
                                        workersActivity.WorkerId,
                                        activities.TimeActivityStart,
                                        activities.TimeActivityEnd
                               FROM Activities activities 
                                inner join WorkersActivity workersActivity on activities.Id = workersActivity.ActivityId
                                where 
                                    activities.IsAlive = 1
                                    and activities.Id <> @ignoreActivityId
                                    and 
                                    not(
                                        
                                        ( @timeActivityStart < activities.TimeActivityStart and @timeActivityEnd < activities.TimeActivityStart)
                                        or
                                        ( @timeActivityStart > activities.TimeActivityEnd and @timeActivityStart > activities.TimeActivityEnd)
                                    ) 
                                    and workersActivity.WorkerId in @workers ";
                                    ;
            var lst = _activityRepository.GetConnection().Query<ActivityWithWorkerDto>(sql, new {workers, timeActivityStart,timeActivityEnd, ignoreActivityId});
            return lst.ToList();
        }
    }
}