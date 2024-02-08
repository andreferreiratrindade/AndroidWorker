using Framework.Core.Mediator;
using Activities.Infra.Data.Repository;
using MediatR;
using Activities.Application.DomainServices;
using Activities.Domain.ValidatorServices;
using Framework.WebApi.Core.Configuration;
using Activities.Application.Commands.AddActivity;
using Activities.Application.Commands.DelteActivity;
using Activities.Application.Commands.DeleteActivity;
using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Activities.Application.Queries;
using Activities.Domain.DTO;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DomainEvents;
using Activities.Application.Events;
using Activities.Infra.Data.Queries;
using Activities.Infra;
using Framework.Core.Data;
using Framework.Core.MongoDb;
using MassTransit;
using Activities.Api.IntegrationServices;
using MassTransit.Configuration;

namespace Activities.Api.Configuration
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
                .RegisterDbContext<ActivityContext>()
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
                // config.AddEntityFrameworkOutbox<ActivityContext>(o =>
                // {
                //     // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                //    o.UsePostgres();

                //     // enable the bus outbox
                //     o.UseBusOutbox();
                // });
                config.AddConsumer<Activity_ActivityAcceptedIntegrationHandle>();
                config.AddConsumer<Activity_ActivityRejectedIntegrationHandle>();
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
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepository, ActivityRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddActivityCommand, AddActivityCommandOutput>, AddActivityCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteActivityCommand, Framework.Core.Messages.Result>, DeleteActivityCommandHandler>();
            services.AddScoped<IRequestHandler<ConfirmActivityCommand, ConfirmActivityCommandOutput>, ConfirmActivityCommandHandler>();


            services.AddScoped<IRequestHandler<GetWorkersActiveNext7DaysQuery, List<WorkActiveReportDto>>, GetWorkersActiveNext7DaysQueryHandler>();

        }

        public static void RegisterRules(this IServiceCollection services)
        {
            services.AddScoped<IActivityValidatorService, ActivityValidatorService>();

        }


        public static void RegisterQueries(this IServiceCollection services)
        {
            services.AddScoped<IActivityQuery, ActivityQuery>();

        }

        public static void RegisterIntegrationService(this IServiceCollection services)
        {

        }

        public static void RegisterEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ActivityCreatedEvent>, ActivityCreatedEventHandler>();
            services.AddScoped<INotificationHandler<ActivityInativatedEvent>, ActivityInativatedEventHandler>();
            services.AddScoped<INotificationHandler<ActivityUptatedTimeStartAndTimeEndEvent>, ActivityUptatedTimeStartAndTimeEndEventHandler>();
        }

        public static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}