

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using System.Security.Authentication.ExtendedProtection;
using Npgsql.Replication;
namespace Framework.Core.OpenTelemetry
{
    public static class OpenTelemetryConfig
    {

        private static void CreateLog(string serviceName)
        {
            Log.Logger = new LoggerConfiguration()
                                            .MinimumLevel.Information()
                                            .Enrich.FromLogContext()
                                            .WriteTo.OpenTelemetry(opts =>
                                            {
                                                opts.IncludedData = IncludedData.SpecRequiredResourceAttributes;
                                                opts.ResourceAttributes = new Dictionary<string, object>
                                                {
                                                    ["app"] = "web",
                                                    ["runtime"] = "dotnet",
                                                    ["service.name"] = serviceName
                                                };
                                            }).CreateLogger();
        }
        public static void RegisterOpenTelemetry(this WebApplicationBuilder builder)
        {
            var serviceName = builder.Configuration.GetSection("NameApp").Value;
            CreateLog(serviceName);
            builder.Host.UseSerilog();
            // builder.Host.UseSerilog((context, loggerConfiguration) =>
            // {

            //      loggerConfiguration.Enrich.WithProperty("Application", "Activity_API")
            //                         .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            //                         .Enrich.WithProperty("service.name", "Activity")
            //                         .WriteTo.Console(new RenderedCompactJsonFormatter())
            //                         .WriteTo.OpenTelemetry(builder.Configuration.GetSection("OpenTelemetryURL").Value);

            //     // loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            //     // loggerConfiguration.WriteTo.Console(Serilog.Events.LogEventLevel.Debug);
            // });

            builder.Services
                .AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName))
                .WithLogging(builderOtel =>
                {
                    builderOtel.AddConsoleExporter();

                    builderOtel.AddOtlpExporter(opts =>
                   {
                       opts.Endpoint = new Uri(builder.Configuration.GetSection("OpenTelemetryURL").Value);
                   });
                })
                .WithTracing(builderOtel =>
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
