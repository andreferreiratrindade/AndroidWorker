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
    public class ActivityContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEventStored _eventStored;

        public ActivityContext(DbContextOptions<ActivityContext> options, IMediatorHandler mediatorHandler,IEventStored eventStored)
           : base(options)
        {
            _mediatorHandler = mediatorHandler;
            _eventStored = eventStored;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        public DbSet<Activity> Activities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.Ignore<AggregateRoot>();

             modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActivityContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            base.OnModelCreating(modelBuilder);
        }
        
        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DtaCreated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DtaCreated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DtaCreated").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso)
            {
                var aggregate  = ChangeTracker.Entries<AggregateRoot>().FirstOrDefault();
                if (aggregate != null)
                {
                    var events = GetEventsByContext();
                    CleanEventsByContext();

                    await _eventStored.SaveAsync(events, aggregate.Entity.AggregateId, "aggregateTemp");
                    await _mediatorHandler.PublishEvent(events);
                }
            }

            return sucesso;
        }

        private List<IDomainEvent> GetEventsByContext( )
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.GetUncommittedChanges().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetUncommittedChanges())
                .ToList();

            return domainEvents;
        }

        private void CleanEventsByContext()
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.GetUncommittedChanges().Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.MarkChangesAsCommitted());
        }
    }

}