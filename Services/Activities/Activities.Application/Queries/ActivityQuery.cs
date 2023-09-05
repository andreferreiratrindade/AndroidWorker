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
        private readonly IEventStored _eventStored;

        public ActivityQuery(IActivityRepository activityRepository, IEventStored eventStored)
        {
            _activityRepository = activityRepository;
            _eventStored = eventStored;
        }

        public async Task<ActivityDto> GetActivityById(Guid activityId){
            var activity = await _activityRepository.GetByIdAsync(activityId) ??  throw new DomainException($"The Activity {activityId} not exists");
            Activity activityRoot;
            //try
            //{
            //    activityRoot = await _eventStored.Get<Activity>(activityId);
            //}catch(Exception ex)
            //{

            //}
            return new ActivityDto{
                ActivityId = activity.AggregateId,
                TimeActivityEnd = activity.TimeActivityEnd,
                TimeActivityStart = activity.TimeActivityStart,
                TypeActivityBuild = activity.TypeActivityBuild,
                WorkerId = activity.GetWorkers().Select(x=>x.WorkerId).ToList()
            };
        }
    }
}