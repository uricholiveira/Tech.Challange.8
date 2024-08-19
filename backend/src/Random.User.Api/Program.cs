using Infrastructure.Data.Database;
using Infrastructure.Data.Seeders;
using Infrastructure.Http.Extensions;
using Infrastructure.Shared.Authentication.Extensions;
using Infrastructure.Shared.Database.Migrations;
using Infrastructure.Shared.Hosting;
using Infrastructure.Shared.Ioc.Extensions;
using Infrastructure.Shared.Mediator.Extensions;
using Infrastructure.Shared.OpenTelemetry.Extensions;
using Microsoft.EntityFrameworkCore;
using Random.User.Application.Extensions;
using Random.User.Domain.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.InitDomain();
builder.Services.InitServices();
var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddDbContextPool<DatabaseContext>(
    o => o.UseNpgsql(
            builder.Configuration.GetConnectionString("DbConnectionString"),
            x => x.MigrationsAssembly("Infrastructure.Data"))
        .EnableSensitiveDataLogging());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.SetupMicroservice(configuration);
builder.Services.AddAuth(configuration);
builder.Services.AddControllers();
builder.Services.AddInject(assemblies);
builder.Services.AddMediator(assemblies);
builder.Services.AddExternalServices(configuration);
builder.Services.RegisterOpenTelemetry(configuration);
builder.Host.UseSerilog();

var app = builder.Build();

app.MigrateDatabase<DatabaseContext>();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    SeedData.Initialize(serviceProvider);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.FinishSetup();
app.UseCors("AllowAll");

var port = Environment.GetEnvironmentVariable("PORT") ?? builder.Configuration["PORT"];

if (string.IsNullOrEmpty(port)) port = "5000";

app.Run($"http://*:{port}");