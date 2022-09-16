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

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(args);
builder.Host.AddEnterpriseApplicationLog("Enterprise:Application:Log");

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.PrimaryAdapters");

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddSingleton<IHostedService, BootstrapperService>(sp =>
{
    XmlApplicationContext context = new CodeConfigApplicationContext()
        .RegisterInstance("Configuration", builder.Configuration)
        .RegisterInstance("ServiceProvider", sp)
        .RegisterInstance("DatabaseSchema", CatalogConstants.Schema)
        .CreateChildContext("./bootstrapper.xml");

    BootstrapperService bootstrapperService = context.GetObject<BootstrapperService>("BootstrapperService");
    //bootstrapperService.AfterExecute += (sender, e) => sp.GetRequiredService<StartupHealthCheck>().StartupCompleted = true;

    return bootstrapperService;
});

builder.Services.AddNHibernate(cfg => cfg
    .Schema(CatalogConstants.Schema)
    .ConnectionStringKey("catalog")
    .AddMappingsFromAssemblyOf<CategoryMapping>()
    .RegisterSession()
    );

builder.Services.AddSingleton(sp => new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Category, CategoryDto>()
        .ForMember(dest => dest.CategoryType, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.CategoryType)))
        .ForMember(dest => dest.Children, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Children)))
        .ForMember(dest => dest.Products, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Products)))
        .ForMember(dest => dest.Description, opt => opt.PreCondition(source => NHibernateUtil.IsPropertyInitialized(source, nameof(source.Description))))
    ;

    cfg.CreateMap<CategoryType, CategoryTypeDto>()
        .ForMember(dest => dest.Categories, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Categories)))
        .ForMember(dest => dest.Description, opt => opt.PreCondition(source => NHibernateUtil.IsPropertyInitialized(source, nameof(source.Description))))


    ;
    cfg.CreateMap<Product, ProductDto>()
        .ForMember(dest => dest.Categories, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Categories)))
        .ForMember(dest => dest.Images, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Images)))
        .ForMember(dest => dest.Description, opt => opt.PreCondition(source => NHibernateUtil.IsPropertyInitialized(source, nameof(source.Description))))
    ;

    cfg.CreateMap<Image, ImageDto>()
        .ForMember(dest => dest.Product, opt => opt.PreCondition(source => NHibernateUtil.IsInitialized(source.Product)))
    ;
}));

builder.Services.AddSingleton(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper());

builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();

builder.Services.AddScoped<IPublicCatalogService, PublicCatalogService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//builder.Services.AddCloudNativeHealthChecks((healthChecksBuilder) =>
//{
//    //healthChecksBuilder.AddRabbitMQ(tags: new[] { "services" }, name: "RabbitMQ", rabbitConnectionString: "");

//    //healthChecksBuilder.AddRedis(tags: new[] { "services" }, name: "Redis", redisConnectionString: "redis:6379");
//});


var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;

        var exceptionHandlerPathFeature =
                context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

        await context.Response.WriteAsync("An exception was thrown." + exceptionHandlerPathFeature?.Error?.ToString() ?? "");

        if (exceptionHandlerPathFeature?.Error != null)
        {
            Console.WriteLine(exceptionHandlerPathFeature.Error.ToString());
        }
    });
});


//app.UseWelcomePage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

//app.UseCloudNativeHealthChecks();



app.Run();


Console.WriteLine("FIM");