

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Framework.Core.OpenTelemetry
{
    public static class OpenTelemetryConfig
    {
        public static void RegisterOpenTelemetry(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(configuration.GetSection("NameApp").Value))
                .WithTracing(builder =>
                {
                    builder
                       .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()

                       //.AddRuntimeInstrumentation()
                       //.AddEntityFrameworkCoreInstrumentation(x => x.SetDbStatementForText = true)

                       .AddOtlpExporter(opts =>
                       {
                           opts.Endpoint = new Uri(configuration.GetSection("OpenTelemetryURL").Value);
                       });
                });
        }
    }
}
