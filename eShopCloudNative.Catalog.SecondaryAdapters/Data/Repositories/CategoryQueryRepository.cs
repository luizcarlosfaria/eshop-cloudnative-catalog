using NHibernate;
using NHibernate.Transform;
using eShopCloudNative.Catalog.Entities;

namespace eShopCloudNative.Catalog.Data.Repositories;

public class CategoryQueryRepository
    : CatalogQueryRepository<Category>,
    ICategoryQueryRepository
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
