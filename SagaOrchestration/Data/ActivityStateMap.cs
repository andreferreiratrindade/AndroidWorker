using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaOrchestration.Models;

namespace SagaOrchestration.Data
{
    public class ActivityStateMap : SagaClassMap<ActivityStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<ActivityStateInstance> entity, ModelBuilder model)
        {
            //entity.Property(x => x.BuyerId).HasMaxLength(256);
            //entity.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
        }
    }
}

{
}
}
