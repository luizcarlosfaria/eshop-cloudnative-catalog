using AutoMapper;
using eShopCloudNative.Architecture.Data;
using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Core;
using System.Data;
using System.Text.Json.Serialization;
using eShopCloudNative.Architecture.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddEnterpriseApplicationLog("Enterprise:Application:Log");

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.WebApi");


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


builder.Services.AddScoped<CategoryQueryRepository>();

builder.Services.AddScoped<IPublicCatalogService, PublicCatalogService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
