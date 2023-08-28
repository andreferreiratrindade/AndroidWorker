using Microsoft.EntityFrameworkCore;
using Activities.Infra;

namespace Activities.Api.Configuration
{
    public static class ApiConfig 
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ActivityContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStringSql"]));
            builder.Services.AddDbContext<ActivityContext>();
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
            builder.Services.AddMessageBusConfiguration(builder.Configuration);
            
            builder.Services.RegisterServices();
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