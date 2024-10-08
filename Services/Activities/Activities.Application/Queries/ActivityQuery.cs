using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DTO;
using Framework.Core.Data;
using Activities.Domain.Models.Entities;

namespace Activities.Application.Queries
{
    public class ActivityQuery : IActivityQuery
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityQuery(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<ActivityDto> GetActivityById(Guid activityId){
            var activity = await _activityRepository.GetByIdAsync(activityId) ??  throw new DomainException($"The Activity {activityId} not exists");

            return new ActivityDto{
                ActivityId = activity.AggregateId,
                TimeActivityEnd = activity.TimeActivityEnd,
                TimeActivityStart = activity.TimeActivityStart,
                TypeActivityBuild = activity.TypeActivityBuild,
                WorkerId = activity.GetWorkers().Select(x=>x.WorkerId).ToList(),
                TypeActivityStatus = activity.Status
            };
        }
    }
}
