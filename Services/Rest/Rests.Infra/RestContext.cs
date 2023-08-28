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
    public class RestContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public RestContext(DbContextOptions<RestContext> options, IMediatorHandler mediatorHandler)
           : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        public DbSet<Rest> Rests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();

             modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestContext).Assembly);

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
            if (sucesso) await _mediatorHandler.PublishEventDbContext(this);

            return sucesso;
        }
    }

}