using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.OpenTelemetry;
using Framework.WebApi.Core.Configuration;
using MediatR;
using Worker.Domain.Models.Data.Queries;
using Worker.Domain.Models.Repositories;
using Worker.Infra.Data.Repository;

namespace Worker.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IDomainNotification, DomainNotification>();
                   builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ApiConfigurationWebApiCore.RegisterServices(builder.Services);
            builder.Services.AddGraphQLServer()
                     .AddQueryType<Query>();
            builder.Services.RegisterIntegrationService();
            builder.Services.RegisterRepositories();
            builder.Services.RegisterCommands();
            builder.Services.RegisterRules();
            builder.Services.RegisterQueries();
            builder.Services.RegisterOpenTelemetry(builder.Configuration);

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
