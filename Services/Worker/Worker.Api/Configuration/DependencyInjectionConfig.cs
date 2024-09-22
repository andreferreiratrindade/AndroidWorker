using Framework.Core.Mediator;
using MediatR;
using Framework.WebApi.Core.Configuration;
using Framework.Core.MongoDb;
using MassTransit;
using MongoDB.Driver;
using Framework.Core.OpenTelemetry;
using Worker.Application.Commands.AddWorker;
using Worker.Domain.Models.Repositories;
using Worker.Infra.Data.Repository;
using ActivityValidationResult.Infra.Data.Mappings;
using Worker.Domain.DomainEvents;
using Worker.Application.Events;
using Worker.Application.IntegrationServices;
using Worker.Domain.Models.Data.Queries;
using Framework.MessageBus;


namespace Worker.Api.Configuration
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
                .AddFiltering()
                .AddSorting();

            builder.Services.RegisterRepositories();
            builder.Services.RegisterCommands();
            builder.Services.RegisterRules();
            builder.Services.RegisterQueries();
            builder.Services.RegisterIntegrationService();
            builder.Services.RegisterEvents();
            builder.RegisterMongoDB();
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

                config.AddConsumer<Worker_ActivityConfirmedIntegrationHandle>();

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
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWorkerRepository, WorkerRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddWorkerCommand, AddWorkerCommandOutput>, AddWorkerCommandHandler>();

        }

        public static void RegisterRules(this IServiceCollection services)
        {

        }


        public static void RegisterQueries(this IServiceCollection services)
        {
        }

        public static void RegisterEvents(this IServiceCollection services)
        {

            services.AddScoped<INotificationHandler<WorkerAddedEvent>, WorkerAddedEventHandler>();

        }


        public static void RegisterMongoDB(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddSingleton<IMongoClient>(_ => {
                var connectionString =
                    builder
                        .Configuration
                        .GetSection("MongoDbConfig:ConnectionString")?
                        .Value;
                return new MongoClient(connectionString);
            });

            WorkerMapping.Configure();
        }
    }
}
