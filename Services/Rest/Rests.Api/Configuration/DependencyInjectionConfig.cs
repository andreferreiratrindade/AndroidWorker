using Framework.Core.Mediator;
using Rests.Infra.Data.Repository;
using Rests.Domain.Models.Repositories;
using MediatR;
using Framework.WebApi.Core.Configuration;
using Rests.Application.Commands.AddRest;
using Framework.Core.Messages;
using Rests.Application.Commands.UpdateTimeStartAndEndRest;
using Framework.Core.Notifications;
using Rests.Domain.ValidationServices;
using Rests.Application.Services;
using Rests.Domain.Models.Data.Queries;
using Rests.Infra;
using Framework.Core.Data;
using Framework.Core.MongoDb;
using Autofac.Core;
using Rests.Api.IntegrationService;
using MassTransit;
using Rests.Application.Events;
using Rests.Domain.DomainEvents;

namespace Rests.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBusConfiguration(builder.Configuration);

            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
            ApiConfigurationWebApiCore.RegisterServices(builder.Services);
            builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .RegisterDbContext<RestContext>()
                .AddFiltering()
                .AddSorting();

            //.AddSubscriptionType<ActivityQuerySubscription>()
            //.AddInMemorySubscriptions();
            builder.Services.RegisterRepositories();
            builder.Services.RegisterCommands();
            builder.Services.RegisterRules();
            builder.Services.RegisterQueries();
            builder.Services.RegisterIntegrationService();
            builder.Services.RegisterEvents();
            builder.RegisterEventStored();
        }
        public static void AddMessageBusConfiguration(this IServiceCollection services,
             IConfiguration configuration)
        {
            var messageQueueConnection = new
            {
                Host = configuration.GetSection("MessageQueueConnection").GetSection("host").Value,
                Username = configuration.GetSection("MessageQueueConnection").GetSection("username").Value,
                Passwoord = configuration.GetSection("MessageQueueConnection").GetSection("password").Value,
            };
            services.AddMassTransit(config =>
            {
                config.AddConsumer<Rest_ActivityCreatedEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(messageQueueConnection.Host, x =>
                    {
                        x.Username(messageQueueConnection.Username);
                        x.Password(messageQueueConnection.Passwoord);

                        cfg.ConfigureEndpoints(ctx);
                    });
                });
            });
        }
        public static void RegisterIntegrationService(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<UpdateTimeStartAndEndRestCommand, Result>, UpdateTimeStartAndEndRestCommandHandler>();
            services.AddScoped<IRequestHandler<AddRestIntegratedCommand, AddRestCommandOutput>, AddRestCommandHandler>();

        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRestRepository, RestRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<UpdateTimeStartAndEndRestCommand, Result>, UpdateTimeStartAndEndRestCommandHandler>();

        }

        public static void RegisterRules(this IServiceCollection services)
        {
            services.AddScoped<IRestValidatorService, RestValidatorService>();

        }


        public static void RegisterQueries(this IServiceCollection services)
        {
        }

        public static void RegisterEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<RestAddedEvent>, RestAddedEventHandler>();
            services.AddScoped<INotificationHandler<RestRejectedEvent>, RestRejectedEventHandler>();
            
        }


        public static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}