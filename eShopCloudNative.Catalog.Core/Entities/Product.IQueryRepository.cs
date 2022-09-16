using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Data.Repositories;
public interface IProductQueryRepository
{

    Task<Product> GetProductAsync(int productId);

    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
}
