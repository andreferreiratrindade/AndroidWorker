using Framework.Core.Mediator;
using Rests.Infra.Data.Repository;
using Rests.Domain.Models.Repositories;
using MediatR;
using Framework.WebApi.Core.Configuration;
using Rests.Application.Commands.AddRest;
using Framework.Core.Messages;
using Rests.Application.Commands.UpdateTimeStartAndEndRest;
using Rests.Domain.ValidationServices;
using Rests.Application.Services;
using Rests.Domain.Models.Data.Queries;
using Rests.Infra;
using Framework.Core.Data;
using Framework.Core.MongoDb;
using MassTransit;
using Rests.Application.Events;
using Rests.Domain.DomainEvents;
using Framework.Core.OpenTelemetry;
using Rests.Application.IntegrationService;
using Framework.MessageBus;

namespace Rests.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBusConfiguration(builder.Configuration);
            builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ApiConfigurationWebApiCore.RegisterServices(builder.Services);
            builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                //.RegisterDbContext<RestContext>()
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
            builder.RegisterOpenTelemetry();
            builder.Services.AddMessageBus();

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
                config.AddEntityFrameworkOutbox<RestContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(1);
                    o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);

                    o.UseBusOutbox();
                });

                config.AddConsumer<Rest_ActivityValidationResultCreatedHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(messageQueueConnection.Host, x =>
                    {
                        x.Username(messageQueueConnection.Username);
                        x.Password(messageQueueConnection.Passwoord);

                        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));
                        cfg.SingleActiveConsumer = true;

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
            services.AddScoped<INotificationHandler<RestAddedCompensationEvent>, RestAddedCompensationHandler>();
        }


        public static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}
