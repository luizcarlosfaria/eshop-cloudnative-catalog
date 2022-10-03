using AutoMapper;
using eShopCloudNative.Architecture.Data;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using NHibernate;
using System.Text.Json.Serialization;
using eShopCloudNative.Architecture.Logging;
using Spring.Context.Support;
using eShopCloudNative.Architecture.Extensions;
using eShopCloudNative.Architecture.Bootstrap;
using eShopCloudNative.Catalog.Data;
using eShopCloudNative.Catalog.Data.Mappings;
using eShopCloudNative.Catalog.Data.Repositories;
using eShopCloudNative.Architecture.HealthChecks;
using RabbitMQ.Client;
using System.Text;
using Serilog;

Console.WriteLine($"Starting {Environment.MachineName}");


Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


var builder = WebApplication.CreateBuilder(args);
bool useHealthChecks = builder.Configuration.GetFlag("boostrap", "healthcheck");
bool useWebApi = builder.Configuration.GetFlag("boostrap", "web-api");


XmlApplicationContext context = new CodeConfigApplicationContext()
        .RegisterInstance("Configuration", builder.Configuration)
        //.RegisterInstance("ServiceProvider", sp)
        .RegisterInstance("DatabaseSchema", CatalogConstants.Schema)
        .CreateChildContext("./bootstrapper.xml");

BootstrapperService bootstrapperService = context.GetObject<BootstrapperService>("BootstrapperService");


builder.Host.AddEnterpriseApplicationLog("Enterprise:Application:Log");

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.Bootstrap");

if (useWebApi)
{
    builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
}

builder.Services.AddSingleton<IHostedService, BootstrapperService>(sp =>
{
    if (useHealthChecks)
    {
        bootstrapperService.AfterExecute += (sender, e) => sp.GetRequiredService<StartupHealthCheck>().StartupCompleted = true;
    }
    return bootstrapperService;
});

builder.Services.AddNHibernate(cfg => cfg
    .Schema(CatalogConstants.Schema)
    .ConnectionStringKey("catalog")
    .AddMappingsFromAssemblyOf<CategoryMapping>()
    .RegisterSession()
    );

if (useWebApi)
{
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    if (useHealthChecks)
    {
        builder.Services.AddCloudNativeHealthChecks((healthChecksBuilder) =>
        {
            //healthChecksBuilder.AddRabbitMQ(tags: new[] { "services" }, name: "RabbitMQ", rabbitConnectionString: "");

            //healthChecksBuilder.AddRedis(tags: new[] { "services" }, name: "Redis", redisConnectionString: "redis:6379");
        });
    }
}

var app = builder.Build();

if (useWebApi)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    if (useHealthChecks)
    {
        app.UseCloudNativeHealthChecks();
    }
}


AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
{
    Log.Error((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
};

app.Run();


Console.WriteLine("FIM");