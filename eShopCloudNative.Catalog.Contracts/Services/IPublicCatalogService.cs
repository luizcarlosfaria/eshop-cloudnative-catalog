using eShopCloudNative.Catalog.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;



public interface IPublicCatalogService
{
    [Get("/Public/HomeCatalog")]
    Task<IEnumerable<CategoryDto>> GetHomeCatalogAsync();


    [Get("/Public/CategoriesForMenu")]
    Task<IEnumerable<CategoryDto>> GetCategoriesForMenuAsync();


    [Get("/Public/Category/{categoryId}")]
    Task<CategoryDto> GetCategoryAsync(int categoryId);


    [Get("/Public/Product/{productId}")]
    Task<ProductDto> GetProductAsync(int productId);

    [Get("/Public/Product/{productId}/price")]
    Task<decimal> GetProductPriceAsync(int productId);

}
