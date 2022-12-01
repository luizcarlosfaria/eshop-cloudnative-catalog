using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("Public")]
[ResponseCache(Duration = 120,  Location = ResponseCacheLocation.Any, NoStore = false)]
public partial class PublicCatalogController : ControllerBase, IPublicCatalogService
{
    private readonly IPublicCatalogService publicCatalog;

    public PublicCatalogController(IPublicCatalogService  publicCatalog)
    {
        this.publicCatalog = publicCatalog;
    }

    [HttpGet("CategoriesForMenu", Name = "CategoriesForMenu")]
    public async Task<IEnumerable<CategoryDto>> GetCategoriesForMenuAsync() 
        => await this.publicCatalog.GetCategoriesForMenuAsync();


    [HttpGet("HomeCatalog", Name = "HomeCatalog")]
    public async Task<IEnumerable<CategoryDto>> GetHomeCatalogAsync() 
        => await this.publicCatalog.GetHomeCatalogAsync();


    [HttpGet("Category/{categoryId}", Name = "GetCategory")]
    public async Task<CategoryDto> GetCategoryAsync(int categoryId)
     => await this.publicCatalog.GetCategoryAsync(categoryId);


    
    [HttpGet("Product/{productId}", Name = "GetProduct")]
    public async Task<ProductDto> GetProductAsync(int productId) 
        => await this.publicCatalog.GetProductAsync(productId);


    [HttpGet("Product/{productId}/price", Name = "GetProductPrice")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<decimal> GetProductPriceAsync(int productId)
       => await this.publicCatalog.GetProductPriceAsync(productId);
}
