using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.Domain.Models.Entities;
using Activities.Infra.Data.Mappings;
using EventStore.ClientAPI;
using Framework.Core.Data;
using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Activities.Infra
{
    public class ActivityContext : DbContextCustom<ActivityContext>
    {

        public ActivityContext(DbContextOptions<ActivityContext> options, IEventStored eventStored)
           : base(options,  eventStored)
        {
        }


        public DbSet<Activity> Activities { get; set; }

    }

}