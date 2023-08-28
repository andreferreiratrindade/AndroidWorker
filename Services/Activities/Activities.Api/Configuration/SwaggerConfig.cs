using System.Reflection;
using Microsoft.OpenApi.Models;
namespace Activities.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
           builder.Services.AddSwaggerGen(config =>
            {   
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tycoon - Android Workers",
                    Description = DESCRIPTION,
                    Contact = new OpenApiContact
                    {
                        Name = "André Ferreira Trindade",
                        Email = "andreferreiratrindade@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/andr%C3%A9-ferreira-trindade-6883846a/")
                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //config.IncludeXmlComments(xmlPath);

            });
            return builder;
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
         public const string DESCRIPTION = @"**Tycoon Co.** is a factory in which the production is realized by automated android workers. You need to create an application 
that maintains and organizes the schedule of these workers' activities inside the factory. 

Each android worker is identified by a letter of the alphabet (A, B, etc).

Workers' activities have a start and end time. There are two different types of activities:
 * 1 - Build component: Which is always performed by one worker.
 * 2 - Build machine: That can be performed by one or several workers that join together in a team.

These android workers can do activities during full day, all days of the year. But each time a worker finishes an activity 
needs to rest some time to recharge its batteries. Independently of the duration of the activity, the charge period will be 
2 hours after finishing to build a component and 4 hours after building a machine.";
    }
}