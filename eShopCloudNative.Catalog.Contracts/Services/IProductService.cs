using eShopCloudNative.Catalog.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;



public interface IProductService
{
    
    [Get("/Products")]
    Task<IEnumerable<Product>> GetProducts();
}
