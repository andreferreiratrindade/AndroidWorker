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

namespace Rests.Api.Configuration
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
                    .AddQueryType<Query>()
                    .RegisterDbContext<RestContext>().AddFiltering()
                .AddSorting();
            services.RegisterIntegrationService();
            services.RegisterRepositories();
            services.RegisterCommands();
            services.RegisterRules();
            services.RegisterQueries();
        }
        public static void RegisterIntegrationService(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<UpdateTimeStartAndEndRestCommand, Result>, UpdateTimeStartAndEndRestCommandHandler>();
            services.AddScoped<IRequestHandler<AddRestCommand, AddRestCommandOutput>, AddRestCommandHandler>();

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
    }
}