using NHibernate;
using NHibernate.Transform;
using eShopCloudNative.Catalog.Entities;
using Serilog;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Prop = Serilog.Core.Enrichers.PropertyEnricher;
using eShopCloudNative.Architecture.Logging;
using eShopCloudNative.Architecture.Data;
using System.Linq.Expressions;

namespace eShopCloudNative.Catalog.Data.Repositories;

public class CategoryQueryRepository
    : CatalogQueryRepository<Category>,
    ICategoryQueryRepository
{
    public CategoryQueryRepository(ISession session) : base(session)
    {
    }

    public Expression<Func<Category, bool>> ActiveOnly = (it => it.Active);


    public async Task<IList<Category>> GetHomeCatalog()
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetHomeCatalog), it => it.AddDataOperation(DataOperation.Query)))
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

    public async Task<IList<Category>> GetCategoriesForMenu()
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetCategoriesForMenu), it => it.AddDataOperation(DataOperation.Query)))
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

    public async Task<Category> GetCategoryAsync(int categoryId)
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetCategoryAsync), 
            it => it
            .AddDataOperation(DataOperation.Query)
            .AddArgument(nameof(categoryId), categoryId)
        ))
        {
            string hql = $@"
            select category
            from {nameof(Category)} as category
            inner join fetch category.{nameof(Category.CategoryType)} as categoryType
            inner join fetch category.{nameof(Category.Products)} as product
            inner join fetch product.{nameof(Product.Images)} as image
            where category.{nameof(Category.CategoryId)} = {categoryId}
            and image.{nameof(Image.Index)} = 0
            ";

            var returnValue = await this.Session.CreateQuery(hql)
            .SetResultTransformer(new DistinctRootEntityResultTransformer())
            .ListAsync<Category>();

            return returnValue.SingleOrDefault();
        }
    }
}
