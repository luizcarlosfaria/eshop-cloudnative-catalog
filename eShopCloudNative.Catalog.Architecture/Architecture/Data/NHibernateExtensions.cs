using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace eShopCloudNative.Catalog.Architecture.Data;
public static class NHibernateExtensions
{
    public static IServiceCollection AddNHibernate<MappingExample>(this IServiceCollection services, string schema, string connectionStringKey)
    {
        return services.AddSingleton(sp =>
         {
             var aspnetConfiguration = sp.GetRequiredService<IConfiguration>();

             return Fluently
              .Configure(new Configuration().SetNamingStrategy(PostgresNamingStragegy.Instance))
              .Database(
                  PostgreSQLConfiguration.PostgreSQL82
                      .ConnectionString(aspnetConfiguration.GetConnectionString(connectionStringKey))
                      .ShowSql()
                      .DefaultSchema(schema)
                  )
              .Mappings(it => it.FluentMappings.AddFromAssemblyOf<MappingExample>())
              .ExposeConfiguration(it => it.SetProperty("hbm2ddl.keywords", "auto-quote"))
              .BuildSessionFactory();

         })
         .AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession())
         .AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenStatelessSession());
    }
}
