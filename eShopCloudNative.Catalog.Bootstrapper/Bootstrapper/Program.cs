using eShopCloudNative.Architecture.Bootstrap;
using eShopCloudNative.Architecture.Extensions;
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


XmlApplicationContext context = new CodeConfigApplicationContext()
        .RegisterInstance("Configuration", configuration)
        .CreateChildContext("./bootstrapper.xml");

var bootstrapperService = context.GetObject<BootstrapperService>("BootstrapperService");

await bootstrapperService.InitializeAsync();

await bootstrapperService.ExecuteAsync();

