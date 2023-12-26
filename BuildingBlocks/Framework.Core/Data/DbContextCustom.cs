using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Data;
using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Core.Data
{
    public abstract class DbContextCustom<TContext> : DbContext, IUnitOfWork
         where TContext : DbContext
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEventStored _eventStored;

        public DbContextCustom(DbContextOptions<TContext> options, IMediatorHandler mediatorHandler, IEventStored eventStored)
           : base(options)
        {
            _mediatorHandler = mediatorHandler;
            _eventStored = eventStored;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.Ignore<AggregateRoot>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            base.OnModelCreating(modelBuilder);
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
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
                var aggregate = ChangeTracker.Entries<AggregateRoot>().FirstOrDefault();
                if (aggregate != null)
                {
                    var events = GetEventsByContext();
                    CleanEventsByContext();

                    await _eventStored.SaveAsync(events, aggregate.Entity.AggregateId, "aggregateTemp");
                    await _mediatorHandler.PublishEvent(events);
                    await base.SaveChangesAsync();
                }
            }

            return sucesso;
        }

        private IEnumerable<IDomainEvent> GetEventsByContext()
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.GetUncommittedChanges().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetUncommittedChanges());

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
