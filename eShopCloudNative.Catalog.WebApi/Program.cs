using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
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

    var configuration = new Configuration()
    .DataBaseIntegration(db =>
    {
        db.ConnectionString = aspnetConfiguration.GetConnectionString("catalog");
        db.Dialect<PostgreSQL84Dialect>();
        db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
        db.IsolationLevel = IsolationLevel.ReadCommitted;
        db.LogSqlInConsole = true;
        db.LogFormattedSql = true;
        db.AutoCommentSql = true;
        db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
        db.ThrowOnSchemaUpdate = true;
        db.Driver<NpgsqlDriver>();
    });

    var mapper = new ModelMapper();

    mapper.AddMappings(typeof(CategoryMapping).Assembly.GetTypes().Where(it => it.IsClass && it.BaseType!.Name.EndsWith("ClassMapping`1")));

    configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

    configuration.SessionFactory().GenerateStatistics();

    var sessionFactory = configuration.BuildSessionFactory();

    //(new SchemaExport(configuration)).Create(true, true);
    //(new SchemaUpdate(configuration)).Execute(true, true);
    //(new SchemaValidator(configuration)).Validate();

    return sessionFactory;
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

session.Query<Category>().ToList();

session.Save(new Category() { Name = "Teste", Slug = "teste", Active = true, Description = "" });
session.Flush();

app.Run();
