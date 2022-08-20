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

builder.Services.AddSingleton(sp =>
{
    var aspnetConfiguration = sp.GetRequiredService<IConfiguration>();

    return Fluently.Configure()
     .Database(
         PostgreSQLConfiguration.PostgreSQL82
             .ConnectionString(aspnetConfiguration.GetConnectionString("catalog"))
             .ShowSql()
             .DefaultSchema(Constants.Schema)
         )
     .Mappings(it => it.FluentMappings.AddFromAssemblyOf<CategoryMapping>())
     .BuildSessionFactory();
});

builder.Services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

builder.Services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenStatelessSession());

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


var session = app.Services.GetRequiredService<ISessionFactory>().OpenSession();

var item = session.Query<Category>().Where(it=> it.CategoryId == 1).SingleOrDefault();
if (item != null)
{
    var x = item.Children.ToList();
}

session.Save(new Category() { Name = "Teste", Slug = "teste", Active = true, Description = "" });
session.Flush();

app.Run();
