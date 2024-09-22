using ApiGateway.Queries.Configurations;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient(WellKnownSchemaNames.Activity,
    c =>c.BaseAddress = new Uri(builder.Configuration.GetSection("activityUrl").Value+"/graphql"));
builder.Services.AddHttpClient(WellKnownSchemaNames.Rest,
    c => c.BaseAddress = new Uri(builder.Configuration.GetSection("restUrl").Value + "/graphql"));
builder.Services.AddHttpClient(WellKnownSchemaNames.Worker,
    c => c.BaseAddress = new Uri(builder.Configuration.GetSection("workerUrl").Value + "/graphql"));
// builder.Services.AddHttpClient(WellKnownSchemaNames.ActivityValidationResult,
//     c => c.BaseAddress = new Uri(builder.Configuration.GetSection("activityValidationResultUrl").Value + "/graphql"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchema(WellKnownSchemaNames.Activity, ignoreRootTypes: true)
    .AddRemoteSchema(WellKnownSchemaNames.Worker, ignoreRootTypes: true)
    .AddRemoteSchema(WellKnownSchemaNames.Rest, ignoreRootTypes: true)
    //.AddRemoteSchema(WellKnownSchemaNames.ActivityValidationResult, ignoreRootTypes: true)
    .AddTypeExtensionsFromFile("./Stitching.graphql");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();



app.Run();
