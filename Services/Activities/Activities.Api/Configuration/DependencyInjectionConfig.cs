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
using HotChocolate.Execution.Processing;
using Activities.Infra;

namespace Activities.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddMediatR(typeof(Program).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            ApiConfigurationWebApiCore.RegisterServices(services);
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .RegisterDbContext<ActivityContext>()
                .AddFiltering()
                .AddSorting();

            //.AddSubscriptionType<ActivityQuerySubscription>()
            //.AddInMemorySubscriptions();
            services.RegisterRepositories();
            services.RegisterCommands();
            services.RegisterRules();
            services.RegisterQueries();
            services.RegisterIntegrationService();
            services.RegisterEvents();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepository, ActivityRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddActivityCommand, AddActivityCommandOutput>, AddActivityCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteActivityCommand, Framework.Core.Messages.Result>, DeleteActivityCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTimeStartAndTimeEndActivityCommand, UpdateTimeStartAndTimeEndActivityCommandOutput>, UpdateTimeStartAndTimeEndActivityCommandHandler>();


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
    }
}