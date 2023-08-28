using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Rests.Domain.DTOs;
using Rests.Domain.Models.Repositories;
using Rests.Infra;
using Rests.Infra.Data.Repository;

namespace Rests.Domain.Models.Data.Queries
{
    public class Query
    {
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<RestDto> AllRestOnly(string workerId, RestContext context)
        {
            var repo = new RestRepository(context);
            var query =  repo.GetQueryable().Where(x => x.WorkerId == workerId).Select(x => new RestDto
            {
                RestId = x.Id,
                ActivityId = x.ActivityId,
                TimeRestEnd = x.TimeRestEnd,
                TimeRestStart = x.TimeRestStart,
                WorkerId = x.WorkerId,
                TypeActivityBuild = x.TypeActivityBuild
            });
            return query;
        }


    }
}