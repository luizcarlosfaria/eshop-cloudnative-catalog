using eShopCloudNative.Catalog.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spring.Context.Support;

var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

XmlApplicationContext context = new XmlApplicationContext("./bootstrapper.xml");

var bootstrapperService = context.GetObject<BootstrapperService>("BootstrapperService");

await bootstrapperService.InitializeAsync(configuration);

await bootstrapperService.ExecuteAsync(configuration);