using Application;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;
using Persistence;
using System;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();

ConfigureServices(builder.Services, configuration, environment);
var app = builder.Build();
// Configure the HTTP request pipeline.
Configure(app, environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
{
    services.AddControllers();

    // Add Swagger services
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "RPPortal API", Version = "v1" });
    });


    // Determine the connection string based on the environment
    string connectionString;
    if (environment.IsDevelopment())
    {
        connectionString = configuration.GetConnectionString("RPPDbLocalConnetionString");

    }
    else if (environment.IsEnvironment("QA"))
    {
        connectionString = configuration.GetConnectionString("RPPDbQAConnectionString");

    }
    else
    {
        connectionString = configuration.GetConnectionString("RPPDbProdConnectionString");

    }
    // EF DbContexts
    var retryAttempts = int.Parse(configuration["DatabaseSettings:RetryAttempts"] ?? "3");
    var retryAttemptsSecondsApart = int.Parse(configuration["DatabaseSettings:RetryAttemptsSecondsApart"] ?? "10");

    services.AddDbContext<RPPDbContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.CommandTimeout(180); // Timeout in seconds (120 seconds = 2 minutes)
            sqlOptions.EnableRetryOnFailure(
                retryAttempts,
                TimeSpan.FromSeconds(retryAttemptsSecondsApart),
                null);
        })
            .ConfigureWarnings(warnings => warnings.Ignore()));

    services.AddHttpClient();


}
void Configure(WebApplication app, IWebHostEnvironment environment)
{
    if (environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Referring Physician Portal - SESS Group");
            c.RoutePrefix = "swagger";
        });
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}