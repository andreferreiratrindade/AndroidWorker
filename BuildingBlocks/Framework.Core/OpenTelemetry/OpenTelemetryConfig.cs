

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace Framework.Core.OpenTelemetry
{
    public static class OpenTelemetryConfig
    {
        public static void RegisterOpenTelemetry(this WebApplicationBuilder builder)
        {
            var serviceName = builder.Configuration.GetSection("NameApp").Value;
            builder.Host.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            });

            // Initialize OTel builder
            var otel = builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName));

            otel.WithTracing(builderOtel =>
            {
                builderOtel
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddRedisInstrumentation()

                    .AddNpgsql()

                   .AddOtlpExporter(opts =>
                   {
                       opts.Endpoint = new Uri(builder.Configuration.GetSection("OpenTelemetryURL").Value);
                   });
            });
        }
    }
}
