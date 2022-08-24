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

builder.Services.AddNHibernate<CategoryMapping>(Constants.Schema, "catalog");

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

var categoryType = session.Query<CategoryType>().Where(it=> it.CategoryTypeId == 1).SingleOrDefault();
if (categoryType == null)
{
    categoryType = new CategoryType() { CategoryTypeId = 1, Name = "CategoryTypeId 1", IsHomeShowCase = true, ShowOnMenu = false };
    session.Save(categoryType);
}


var item = session.Query<Category>().Where(it=> it.CategoryId == 1).SingleOrDefault();
if (item != null)
{
    Console.WriteLine($"Root {item.CategoryId}");
    Console.WriteLine($"BEGIN Children of {item.CategoryId}");
    if (item.Children != null)
        foreach (var child in item.Children)
        {
            Console.WriteLine($"    Child {child.CategoryId}");
        }
    Console.WriteLine($"END Children of {item.CategoryId}");
}

var categoryToCreate = new Category() { Name = "Category Teste 1", Slug = "teste", Type = categoryType, Active = true, Description = "" };
var productToCreate = new Product() { Name = "Product Teste 1", Slug = "teste", Active = true, Description = "", Price = 5, Categories = new List<Category>() {  } };

if (item != null && item.Children != null && item.Children.Count > 0)
{
    productToCreate.Categories.Add(item.Children.First());
}


session.Save(categoryToCreate);
session.Save(productToCreate);
session.Flush();

app.Run();
