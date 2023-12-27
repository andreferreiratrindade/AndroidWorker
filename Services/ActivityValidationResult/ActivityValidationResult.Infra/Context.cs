using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityValidationResult.Domain.Models.Entities;
using ActivityValidationResult.Infra.Data.Mappings;
using Framework.Core.Data;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace ActivityValidationResult.Infra
{
    public class Context : DbContextCustom<Context>
    {

        public Context(DbContextOptions<Context> options, IMediatorHandler mediatorHandler, IEventStored eventStored)
           : base(options, mediatorHandler, eventStored)
        {

        }


        public DbSet<ActivityValidationResultEntity> ActivityValidationResult { get; set; }
    }

}