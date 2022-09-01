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

    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts()
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

}
