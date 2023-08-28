using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Data;
using Activities.Domain.Models.Entities;

namespace Activities.Domain.Models.Repositories
{
    public interface IActivityRepository : IRepository<Activity>
    {
        void Update(Activity activity);
        void Add(Activity activity);
        void Delete(Guid activityId);
        Activity? GetById(Guid activityId);
        Task<Activity?> GetByIdAsync(Guid activityId);

        IQueryable<Activity> GetQueryable();
    }
}