using eShopCloudNative.Architecture.Bootstrap;
using eShopCloudNative.Architecture.Extensions;
using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Spring.Context.Support;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddEnterpriseApplicationLog("Enterprise:Application:Log");

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.Bootstrapper");


builder.Services.AddSingleton<IHostedService>(sp =>
{

    XmlApplicationContext context = new CodeConfigApplicationContext()
        .RegisterInstance("Configuration", builder.Configuration)
        .RegisterInstance("ServiceProvider", sp)
        .RegisterInstance("DatabaseSchema", CatalogConstants.Schema)
        .CreateChildContext("./bootstrapper.xml");

    return context.GetObject<BootstrapperService>("BootstrapperService");

});

var app = builder.Build();

app.Run();

Log.Information("Bootstrap realizado com sucesso!");
