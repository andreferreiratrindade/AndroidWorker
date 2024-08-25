using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Mediator;

public static class MediatorBehaviorConfig
{
    public static void RegisterMediatorBehavior(this IServiceCollection services, System.Reflection.Assembly assembly)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandValidateBehavior<,>));
        });
        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }
}
