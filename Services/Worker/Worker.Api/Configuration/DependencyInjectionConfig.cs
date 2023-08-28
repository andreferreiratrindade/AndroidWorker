using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.WebApi.Core.Configuration;
using MediatR;
using Worker.Domain.Models.Data.Queries;
using Worker.Domain.Models.Repositories;
using Worker.Infra.Data.Repository;

namespace Worker.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IDomainNotification, DomainNotification>();
            services.AddMediatR(typeof(Program).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            ApiConfigurationWebApiCore.RegisterServices(services);
            services.AddGraphQLServer()
                     .AddQueryType<Query>();
            services.RegisterIntegrationService();
            services.RegisterRepositories();
            services.RegisterCommands();
            services.RegisterRules();
            services.RegisterQueries();
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

        }

        public static void RegisterRules(this IServiceCollection services)
        {

        }


        public static void RegisterQueries(this IServiceCollection services)
        {
        }
    }
}