using FluentMigrator;
using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Architecture.Data;
public static class MappingExtensions
{
    public static ICreateTableWithColumnSyntax Table(this ICreateExpressionRoot it, string tableName, string schema) 
        => it.Table(tableName.UnderQuotes()).InSchema(schema.UnderQuotes());

    public static string UnderQuotes(this string text) => $"\"{text}\"";

    public static ICreateTableColumnOptionOrWithColumnSyntax Map<T>(this ICreateTableWithColumnSyntax it, Expression<Func<T, int>> memberExpression)
        => 
        it.WithColumn(memberExpression.GetPropertyName().UnderQuotes())
        .AsInt32();

    public static ICreateTableColumnOptionOrWithColumnSyntax Map<T>(this ICreateTableWithColumnSyntax it, Expression<Func<T, long>> memberExpression)
     =>
     it.WithColumn(memberExpression.GetPropertyName().UnderQuotes())
     .AsInt64();

    public static ICreateTableColumnOptionOrWithColumnSyntax Map<T>(this ICreateTableWithColumnSyntax it, Expression<Func<T, string>> memberExpression, int length)
       =>
       it.WithColumn(memberExpression.GetPropertyName().UnderQuotes())
       .AsString(length);

    public static ICreateTableColumnOptionOrWithColumnSyntax Map<T>(this ICreateTableWithColumnSyntax it, Expression<Func<T, bool>> memberExpression)
           =>
           it.WithColumn(memberExpression.GetPropertyName().UnderQuotes())
           .AsBoolean();

    public static ICreateTableColumnOptionOrWithColumnSyntax Map<T>(this ICreateTableWithColumnSyntax it, Expression<Func<T, decimal>> memberExpression, int size, int precision)
       =>
       it.WithColumn(memberExpression.GetPropertyName().UnderQuotes())
       .AsDecimal(size, precision);

    private static string GetPropertyName<T1,T2>(this Expression<Func<T1, T2>> property)
    {
        LambdaExpression lambda = (LambdaExpression)property;
        MemberExpression memberExpression;

        if (lambda.Body is UnaryExpression)
        {
            UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
            memberExpression = (MemberExpression)(unaryExpression.Operand);
        }
        else
        {
            memberExpression = (MemberExpression)(lambda.Body);
        }

        return ((PropertyInfo)memberExpression.Member).Name;
    }
}
