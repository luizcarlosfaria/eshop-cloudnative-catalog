using eShopCloudNative.Architecture.Logging;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using NHibernate;
using NHibernate.Transform;
using Serilog;
using Serilog.Context;
using Serilog.Core.Enrichers;
using System.Linq.Expressions;

namespace eShopCloudNative.Catalog.Data.Repositories;

public class ProductQueryRepository :
    CatalogQueryRepository<Product>,
    IProductQueryRepository
{
    public ProductQueryRepository(ISession session) : base(session)
    {
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetProductAsync), it => it.Add("Operation", "Query")))
        {
            //TODO: Query com problemas - Resultado duplicando Categorias

            var returnValue = await this.QueryOver
            .Fetch(SelectMode.FetchLazyProperties, it => it)
            .Fetch(it => it.Images, it => it.Categories)
            .Where(it => it.ProductId == productId)
            .SingleOrDefaultAsync();

            returnValue.Categories = returnValue.Categories.Distinct().ToList();
            returnValue.Images = returnValue.Images.Distinct().ToList();

            //var returnValue = await this.Session.GetAsync<Product>(productId);
            //returnValue.Categories.Any();
            //returnValue.Images.Any();

            return returnValue;
        }
    }

    public async Task<decimal> GetProductPriceAsync(int productId)
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetProductPriceAsync), it => it.Add("Operation", "Query")))
        {
            return (await this.QueryOver
            .Where(it => it.ProductId == productId)
            .Select(it => it.Price)
            .SingleOrDefaultAsync<decimal>());
        }
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        using (new EnterpriseApplicationLogContext(nameof(ProductQueryRepository), nameof(GetProductsByCategoryAsync), it => it.Add("Operation", "Query")))
        {
            return await this.QueryOver.Where(p =>
                p.Categories != null
                &&
                p.Categories.Any(c => c.CategoryId == categoryId)
            ).ListAsync();
        }
    }

}
