using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrchestration.Data
{
    public class ActivityStateDbContext : SagaDbContext
    {
        public ActivityStateDbContext(DbContextOptions<ActivityStateDbContext> options) : base(options) { }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new ActivityStateMap(); }
        }
    }
}