using eShopCloudNative.Catalog.Infrastructure.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
internal class ProductQueryRepository : QueryRepository<Product>
{
    public ProductQueryRepository(IStatelessSession session) : base(session)
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
