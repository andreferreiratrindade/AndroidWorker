using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.Domain.DTO;

namespace Activities.Application.Queries
{
    public interface IActivityQuery
    {
        Task<ActivityDto> GetActivityById(Guid activityId);
    }
}