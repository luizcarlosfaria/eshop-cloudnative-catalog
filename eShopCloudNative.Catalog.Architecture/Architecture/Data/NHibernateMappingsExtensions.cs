using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eShopCloudNative.Catalog.Architecture.Data;
public static partial class NHibernateMappingsExtensions
{
    public static void Configure<TEntity>(this ClassMapping<TEntity> mapping, string table, string schema = null)
          where TEntity : class
    {
        if (schema != null)
            mapping.Schema(schema);

        mapping.Table(table);
    }

    public static void MapId<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, object>> property, string columnName = null, IGeneratorDef generator = null)
        where TEntity : class
    {
        generator ??= Generators.Native;

        columnName ??= ((MemberExpression)property.Body).Member.Name;

        mapping.Id(property, map =>
        {
            map.Column(columnName);
            map.Generator(generator);
        });
    }

    public static void MapSequenceId<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, object>> property, string columnName, string sequenceName)
        where TEntity : class
    {

        sequenceName ??= $"{nameof(TEntity)}_{columnName}_seq";

        mapping.Id(property, map =>
        {
            map.Column(columnName);
            map.Generator(Generators.Sequence, it => it.Params(new { sequence = sequenceName }));
        });
    }




    public static void Map<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, string>> property, bool nullable, int length, string columnName = null)
        where TEntity : class
    {
        mapping.Property(property, map =>
        {
            map.NotNullable(!nullable);
            map.Length(length);

            if (columnName != null)
                map.Column(columnName);
        });
    }

    public static void Map<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, decimal>> property, bool nullable, short precision = 5, short scale = 2, string columnName = null)
        where TEntity : class
    {
        mapping.Property(property, map =>
        {
            map.NotNullable(!nullable);

            map.Precision(precision);
            map.Scale(scale);

            if (columnName != null)
                map.Column(columnName);
        });
    }

    public static void Map<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, bool>> property, bool nullable, string columnName = null)
       where TEntity : class
    {
        mapping.Property(property, map =>
        {
            map.NotNullable(!nullable);

            if (columnName != null)
                map.Column(columnName);
        });
    }


    public static void MapClob<TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, string>> property, bool nullable, string columnName = null)
        where TEntity : class
    {
        columnName ??= ((MemberExpression)property.Body).Member.Name;

        mapping.Property(property, map =>
        {
            map.NotNullable(!nullable);
            map.Length(1073741823); //TODO: magic number Text para Postgresql
            map.Lazy(true);
            map.Column(columnName);
        });
    }


    public static void MapClob<TEntity>(this IComponentMapper<TEntity> mapping, Expression<Func<TEntity, string>> property, bool nullable, string columnName = null)
    //where TEntity : class
    {
        columnName ??= ((System.Linq.Expressions.MemberExpression)property.Body).Member.Name;

        mapping.Property(property, map =>
        {
            map.NotNullable(!nullable);
            map.Length(1073741823); //TODO: magic number Text para Postgresql
            map.Lazy(true);
            map.Column(columnName);
        });
    }

    public static void MapManyToOne<TProperty, TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, TProperty>> property, bool nullable, string FKName = null, string columnName = null)
       where TEntity : class
       where TProperty : class
    {
        mapping.ManyToOne(property, map =>
        {
            if (FKName == null)
            {
                var currentEntityType = typeof(TEntity);
                var referenceEntityType = typeof(TProperty);
                map.ForeignKey($"FK_{referenceEntityType.Name}_TO_{currentEntityType.Name}");
            }
            else
            {
                map.ForeignKey(FKName);
            }

            if (columnName == null)
            {
                var currentEntityType = typeof(TEntity);
                var referenceEntityType = typeof(TProperty);
                map.Column($"{referenceEntityType.Name}Id");
            }
            else
            {
                map.Column(columnName);
            }

            map.NotNullable(!nullable);
        });
    }

    public static void MapOneToMany<TProperty, TEntity>(this ClassMapping<TEntity> mapping, Expression<Func<TEntity, IEnumerable<TProperty>>> listProperty, Expression<Func<TEntity, Object>> keyProperty, string columnName = null)
       where TEntity : class
       where TProperty : class
    {
        var currentEntityType = typeof(TEntity);
        var referenceEntityType = typeof(TProperty);

        mapping.List(listProperty, map =>
        {
            columnName ??= ((MemberExpression)keyProperty.Body).Member.Name;

            map.Key(keyconfig => {
                keyconfig.Column(columnName);
                keyconfig.ForeignKey($"FK_{currentEntityType.Name}_TO_{referenceEntityType.Name}");
            });

        });
    }


}
