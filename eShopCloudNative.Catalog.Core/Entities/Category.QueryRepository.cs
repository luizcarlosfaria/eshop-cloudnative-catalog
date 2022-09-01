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

    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts6()
    {
        string hql = @"
            SELECT 
            {c.*},
            {p.*},
            i.*
            FROM ""catalog"".""Category"" c 
            inner join ""catalog"".""CategoryType"" ct on c.""CategoryTypeId"" = ct.""CategoryTypeId"" 
            inner join ""catalog"".""Product_Category"" pc on c.""CategoryId"" = pc.""CategoryId"" 
            inner join ""catalog"".""Product"" p on pc.""ProductId"" = p.""ProductId"" 
            inner join ""catalog"".""Image"" i on p.""ProductId"" = i.""ProductId"" 
            where ct.""IsHomeShowCase"" = true;
        ";
        var returnValue = this.Session.CreateSQLQuery(hql)
            .AddEntity("c", typeof(Category))
            .AddEntity("p", typeof(Product))
            .AddJoin("p", "c.Products")

            //.AddEntity("i", typeof(Image))
            
            //.AddJoin("i", "p.Images")
            //.SetResultTransformer(new DistinctRootEntityResultTransformer())
            .SetResultTransformer(new AliasToBeanResultTransformer(typeof(Category)))
            .List();

        return null;
    }

    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts5()
    {
        Category category = null;
        CategoryType categoryType = null;
        Product product = null;
        Image image = null;

        var returnValue = this.Session.QueryOver<Category>(() => category)
            .Fetch(SelectMode.Fetch, it => it.Products)
            .JoinQueryOver(it => it.CategoryType, () => categoryType, NHibernate.SqlCommand.JoinType.InnerJoin, Restrictions.Where<CategoryType>(it => it.IsHomeShowCase == true))
            .TransformUsing(new DistinctRootEntityResultTransformer())
            .List<Category>();

        return returnValue;
    }

    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts4()
    {
        string sql = @"
            SELECT 
            c.""CategoryId"",c.""ParentCategoryId"",c.""CategoryTypeId"",c.""Name"",c.""Description"",c.""Icon"",c.""Slug"",c.""Active"", 
            p.""ProductId"",p.""Name"",p.""Description"",p.""Slug"",p.""Price"",p.""Active"",
            i.""ImageId"",i.""FileName""

            FROM ""catalog"".""Category"" c 
            inner join ""catalog"".""CategoryType"" ct on c.""CategoryTypeId"" = ct.""CategoryTypeId"" 
            inner join ""catalog"".""Product_Category"" pc on c.""CategoryId"" = pc.""CategoryId"" 
            inner join ""catalog"".""Product"" p on pc.""ProductId"" = p.""ProductId"" 
            inner join ""catalog"".""Image"" i on p.""ProductId"" = i.""ProductId"" 
            where ct.""IsHomeShowCase"" = true;
        ";
        var returnValue = this.Session.CreateSQLQuery(sql)
            .AddEntity("c", typeof(Category))
            .AddJoin("p", "c.Products")
            .AddJoin("i", "p.Images")
            .SetResultTransformer(new DistinctRootEntityResultTransformer())
            .List<Category>();

        return returnValue;
    }


    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts()
    {
        string hql = @"
            select category
            from Category as category
            inner join fetch category.CategoryType as categoryType
            inner join fetch category.Products as product
            inner join fetch product.Images as image
            where categoryType.IsHomeShowCase = true
            and image.Index = 0
        ";
        var returnValue = this.Session.CreateQuery(hql).SetResultTransformer(new DistinctRootEntityResultTransformer()).List<Category>();

        return returnValue;
    }

    public async Task<IList<Category>> GetShowCaseCategoriesWithProducts1()
    {
        Category category = null;
        CategoryType categoryType = null;
        Product product = null;
        Image image = null;

        var returnValue = await this.Session.QueryOver<Category>(() => category)
            .JoinQueryOver(it => it.CategoryType, () => categoryType, NHibernate.SqlCommand.JoinType.InnerJoin, Restrictions.Where<CategoryType>(it => it.IsHomeShowCase == true))
            .Cacheable()
            .CacheRegion("showcase")
            //.Fetch(SelectMode.Fetch, () => category.Products)
            .ListAsync();
        return returnValue;
    }

    private async Task<IList<Category>> GetShowCaseCategoriesWithProducts2()
    {
        Product product = null;
        Image image = null;

        var returnValue = await this.QueryOver
            //.JoinAlias(it => it.CategoryType, () => categoryType)
            .JoinAlias(it => it.Products, () => product)
            .JoinAlias(() => product.Images, () => image)
            .JoinQueryOver(it => it.CategoryType).Where(it => it.IsHomeShowCase == true)
            .Cacheable()
            .CacheRegion("showcase")
            .ListAsync();

        return returnValue;
    }
}
