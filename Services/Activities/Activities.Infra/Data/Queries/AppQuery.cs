using Activities.Domain.DTO;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Infra.Data.Repository;
using HotChocolate.Subscriptions;
using System.Diagnostics;

namespace Activities.Infra.Data.Queries
{
    public class Query
    {

        public IQueryable<ActivityDto> AllActivityOnly(Guid activityId,  ActivityContext context)
        {
            var activityRepository = new ActivityRepository(context);
            var query = activityRepository.GetQueryable().Where(x=> x.AggregateId == activityId).Select(x => new ActivityDto
            {
                ActivityId = x.AggregateId,
                TimeActivityEnd = x.TimeActivityEnd,
                TimeActivityStart = x.TimeActivityStart,
                TypeActivityBuild = x.TypeActivityBuild
            }).AsQueryable();


            return query;
        }
    }
}
