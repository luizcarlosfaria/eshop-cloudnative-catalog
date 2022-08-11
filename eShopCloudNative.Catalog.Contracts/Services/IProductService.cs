using eShopCloudNative.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
}
