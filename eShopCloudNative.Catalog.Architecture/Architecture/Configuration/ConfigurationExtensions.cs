using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Architecture.Configuration;
public static class ConfigurationExtensions
{
    public static T Factory<T>(this IConfiguration configuration, string key) 
        => configuration.Initialize(key, Activator.CreateInstance<T>());

    public static T Initialize<T>(this IConfiguration configuration, string key, T item)
    {
        configuration.Bind(key, item);
        return item;
    }
}
