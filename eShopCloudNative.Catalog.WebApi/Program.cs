    using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddNHibernate(cfg => cfg
    .Schema(Constants.Schema)
    .ConnectionStringKey("catalog")
    .AddMappingsFromAssemblyOf<CategoryMapping>()
    .RegisterSession()
    .RegisterStatelessSession()
    );

builder.Services.AddScoped<IProductService, ProductService>();

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
