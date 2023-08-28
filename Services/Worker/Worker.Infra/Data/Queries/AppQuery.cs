using HotChocolate.Subscriptions;
using Worker.Domain.DTOs;
using Worker.Domain.Models.Repositories;

namespace Worker.Domain.Models.Data.Queries
{
    public class Query
    {
        //[UsePaging]
        //[UseFiltering]
        //[UseSorting]
        //public async Task<IQueryable<WorkerDto>> AllWorkerOnly([Service] IWorkerRepository workerRepository, [Service] ITopicEventSender eventSender)
        //{
        //    var query = workerRepository.GetQueryable().Select(x => new WorkerDto
        //    {
        //        WorkerId = x.WorkerId
        //    }).AsQueryable();

        //    await eventSender.SendAsync("Returned a list of Workers",
        //      query);
        //    return query;
        //}

        public IQueryable<WorkerDto> AllWorkerOnly([Service] IWorkerRepository workerRepository)
        {
            var query = workerRepository.GetQueryable().Select(x => new WorkerDto
            {
                WorkerId = x.WorkerId
            }).AsQueryable();

            return query;
        }

    }
}