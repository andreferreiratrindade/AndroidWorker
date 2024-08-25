using Framework.Core.DomainObjects;
using global::Framework.WebApi.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Framework.WebApi.Core.Filters;
using Microsoft.AspNetCore.Mvc;
using Framework.Core.Notifications;

namespace Framework.WebApi.Core.Configuration
{
    public static class ApiConfigurationWebApiCore
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDomainNotification, DomainNotification>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                //options.Filters.Add(typeof(DomainNotificationFilter));

                options.EnableEndpointRouting = false;
            });

            services.AddMvc(options => options.Filters.Add<DomainNotificationFilter>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //.AddNewtonsoftJson();
            //services.AddProblemDetails(x => x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex)));
        }
    }
}
