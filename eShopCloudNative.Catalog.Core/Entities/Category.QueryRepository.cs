using eShopCloudNative.Catalog.Architecture.Data.Repositories;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using NHibernate.Hql;
using NHibernate.Hql.Ast;
using NHibernate.Hql.Util;
using NHibernate.Linq;
using NHibernate.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;

namespace eShopCloudNative.Catalog.Entities;




public class CategoryQueryRepository : QueryRepository<Category>
{
    public CategoryQueryRepository(ISession session) : base(session)
    {
    }

    public async Task<IList<Category>> GetHomeCatalog()
    {
        string hql = $@"
            select category
            from {nameof(Category)} as category
            inner join fetch category.{nameof(Category.CategoryType)} as categoryType
            inner join fetch category.{nameof(Category.Products)} as product
            inner join fetch product.{nameof(Product.Images)} as image
            where categoryType.{nameof(CategoryType.IsHomeShowCase)} = true
            and image.{nameof(Image.Index)} = 0
        ";

        var returnValue = await this.Session.CreateQuery(hql)
            .SetResultTransformer(new DistinctRootEntityResultTransformer())
            .ListAsync<Category>();

        return returnValue;
    }

    public async Task<IList<Category>> GetCategoriesForMenu()
    {
        string hql = $@"
            select category
            from {nameof(Category)} as category
            inner join fetch category.{nameof(Category.CategoryType)} as categoryType
            left join fetch category.{nameof(Category.Parent)} as parent
        ";

        var categories = await this.Session.CreateQuery(hql)
            .SetResultTransformer(new DistinctRootEntityResultTransformer())
            .ListAsync<Category>();

        foreach (var category in categories)
        {
            category.Children = categories
                .Where(it => 
                it.Parent != null 
                && it.Parent.CategoryId == category.CategoryId
                && it.CategoryType.ShowOnMenu
                ).ToList();
        }

        return categories.Where(it => 
            it.CategoryType.ShowOnMenu 
            && it.Parent == null)
            .ToList();
    }
}
