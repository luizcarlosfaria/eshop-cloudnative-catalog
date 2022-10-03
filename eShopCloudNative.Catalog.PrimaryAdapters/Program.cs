using AutoMapper;
using eShopCloudNative.Architecture.Data;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using NHibernate;
using System.Text.Json.Serialization;
using eShopCloudNative.Architecture.Logging;
using eShopCloudNative.Architecture.Extensions;
using eShopCloudNative.Catalog.Data;
using eShopCloudNative.Catalog.Data.Mappings;
using eShopCloudNative.Catalog.Data.Repositories;
using eShopCloudNative.Architecture.HealthChecks;
using System.Text;
using Serilog;

Console.WriteLine($"Starting {Environment.MachineName}");


Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


var builder = WebApplication.CreateBuilder(args);
bool useHealthChecks = builder.Configuration.GetFlag("boostrap", "healthcheck");


builder.AddEnterpriseApplicationLog("Enterprise:Application:Log", Mode.Standalone);

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.PrimaryAdapters");

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
builder.Services.AddScoped<IPublicCatalogService, PublicCatalogService>();

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


var app = builder.Build();


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

if (useHealthChecks)
{
    app.UseCloudNativeHealthChecks();
}

AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
{
    Log.Error((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
};

app.Run();


Console.WriteLine("FIM");