using Microsoft.EntityFrameworkCore;
using Rests.Infra;

namespace Rests.Api.Configuration
{
    public static class ApiConfig 
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RestContext>(options =>
       options.UseSqlServer(builder.Configuration["ConnectionStringSql"]));
            builder.Services.AddDbContext<RestContext>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("all",
                   builder =>
                       builder
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
           

            builder.RegisterServices();
            return builder;

        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.UseRouting();

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.MapGraphQL();

            //  app.MapControllers();
            return app;
        }
    }
}