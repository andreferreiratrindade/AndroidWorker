using Framework.Core.Mediator;
using ActivityValidationResult.Infra.Data.Repository;
using ActivityValidationResult.Domain.Models.Repositories;
using MediatR;
using Framework.WebApi.Core.Configuration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using Framework.Core.MongoDb;
using MassTransit;
using ActivityValidationResult.Application.Events;
using ActivityValidationResult.Domain.DomainEvents;
using MongoDB.Driver;
using ActivityValidationResult.Infra.Data.Mappings;
using ActivityValidationResult.Application.Commands.AddRestAcceptedActivityValidationResult;
using ActivityValidationResult.Application.Commands.AddRestRejectedActivityValidationResult;
using Framework.Core.OpenTelemetry;
using ActivityValidationResult.Application.IntegrationService;
using Framework.MessageBus;

namespace ActivityValidationResult.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBusConfiguration(builder.Configuration);

            builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ApiConfigurationWebApiCore.RegisterServices(builder.Services);


            //.AddSubscriptionType<ActivityQuerySubscription>()
            //.AddInMemorySubscriptions();
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
                config.AddConsumer<ActivitiyValidationResult_ActivityCreatedEventHandler>();
                config.AddConsumer<ActivityValidationResult_RestAddedEventHandler>();
                config.AddConsumer<ActivityValidationResult_RestRejectedEventHandler>();
                config.AddConsumer<ActivitiyValidationResult_ActivityConfirmedEventHandler>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(messageQueueConnection.Host, x =>
                    {
                        x.Username(messageQueueConnection.Username);
                        x.Password(messageQueueConnection.Passwoord);
                        cfg.SingleActiveConsumer = true;

                        cfg.ConfigureEndpoints(ctx);
                    });
                });
            });
        }
        public static void RegisterIntegrationService(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput>, AddActivityValidationResultCommandHandler>();
            services.AddScoped<IRequestHandler<AddRestAcceptedActivityValidationResultCommand, AddRestAcceptedActivityValidationResultCommandOutput>, AddRestAcceptedActivityValidationResultCommandHandler>();
            services.AddScoped<IRequestHandler<AddRestRejectedActivityValidationResultCommand, AddRestRejectedActivityValidationResultCommandOutput>, AddRestRejectedActivityValidationResultCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActivityConfirmedCommand, UpdateActivityConfirmedCommandOutput>, UpdateActivityConfirmedCommandHandler>();

        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActivityValidationResultRepository, ActivityValidationResultRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {

        }

        public static void RegisterRules(this IServiceCollection services)
        {

        }


        public static void RegisterQueries(this IServiceCollection services)
        {
        }

        public static void RegisterEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ActivityValidationResultAddedEvent>, ActivityValidationAddedEventHandler>();
            services.AddScoped<INotificationHandler<ActivityValidationResultAddedCompensationEvent>, ActivityValidatioAddedCompensationEventHandler>();
            services.AddScoped<INotificationHandler<ActivityAcceptedEvent>, ActivityAcceptedEventHandler>();
            services.AddScoped<INotificationHandler<ActivityRejectedEvent>, ActivityRejectedEventHandler>();

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

            ActivityValidationResultMapping.Configure();
        }
    }
}
