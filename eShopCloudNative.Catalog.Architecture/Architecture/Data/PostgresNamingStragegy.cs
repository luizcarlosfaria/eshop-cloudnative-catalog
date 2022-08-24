using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NHibernate.Cfg.Mappings;

namespace eShopCloudNative.Catalog.Architecture.Data;
public class PostgresNamingStragegy : INamingStrategy
{
    private static readonly INamingStrategy improvedNamingStrategy = NHibernate.Cfg.ImprovedNamingStrategy.Instance;

    private static PostgresNamingStragegy instance;
    public static INamingStrategy Instance
    {
        get { return instance ?? (instance = new PostgresNamingStragegy()); }
    }

    protected PostgresNamingStragegy()
    {
    }

    public string ClassToTableName(string className)
    {
        return improvedNamingStrategy.ClassToTableName(className);
    }

    public string ColumnName(string columnName)
    {
        return "\"" + columnName + "\"";
    }
    public string TableName(string tableName)
    {
        return "\"" + tableName + "\"";
    }

    public string LogicalColumnName(string columnName, string propertyName)
    {
        return improvedNamingStrategy.LogicalColumnName(columnName, propertyName);
    }

    public string PropertyToColumnName(string propertyName)
    {
        return improvedNamingStrategy.PropertyToColumnName(propertyName);
    }

    public string PropertyToTableName(string className, string propertyName)
    {
        return improvedNamingStrategy.PropertyToTableName(className, propertyName);
    }

   
}