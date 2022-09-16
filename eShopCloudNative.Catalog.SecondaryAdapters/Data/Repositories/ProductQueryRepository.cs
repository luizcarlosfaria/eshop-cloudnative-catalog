using eShopCloudNative.Catalog.Entities;
using NHibernate;

namespace eShopCloudNative.Catalog.Data.Repositories;

public class ProductQueryRepository : 
    CatalogQueryRepository<Product>, 
    IProductQueryRepository
{
    public ProductQueryRepository(ISession session) : base(session)
    {
    }

    public async Task<Product> GetProductAsync(int productId)
        => await this.QueryOver.Where(it => it.ProductId == productId).SingleOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        => await this.QueryOver.Where(p =>
            p.Categories != null
            &&
            p.Categories.Any(c => c.CategoryId == categoryId)
        ).ListAsync();

}
