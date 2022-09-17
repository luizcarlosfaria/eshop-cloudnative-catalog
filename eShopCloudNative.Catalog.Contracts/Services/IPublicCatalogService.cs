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
    [Get("/Public/Catalog/HomeCatalog")]
    Task<IEnumerable<CategoryDto>> GetHomeCatalogAsync();


    [Get("/Public/Catalog/CategoriesForMenu")]
    Task<IEnumerable<CategoryDto>> GetCategoriesForMenuAsync();


    [Get("/Public/Catalog/Category/{categoryId}")]
    Task<CategoryDto> GetCategoryAsync(int categoryId);


    [Get("/Public/Catalog/Product/{productId}")]
    Task<ProductDto> GetProductAsync(int productId);

}
