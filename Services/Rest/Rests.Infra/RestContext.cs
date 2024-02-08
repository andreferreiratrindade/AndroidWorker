using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rests.Domain.Models.Entities;
using Rests.Infra.Data.Mappings;
using Framework.Core.Data;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Rests.Infra
{
    public class RestContext : DbContextCustom<RestContext>
    {

        public RestContext(DbContextOptions<RestContext> options, IMediatorHandler mediatorHandler, IEventStored eventStored)
           : base(options, eventStored)
        {

        }


        public DbSet<Rest> Rests { get; set; }
    }

}