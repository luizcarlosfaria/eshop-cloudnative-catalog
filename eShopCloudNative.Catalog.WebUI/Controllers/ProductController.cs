using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;

[Route("/p")]
public class ProductController : EShopControllerBase
{
    public ProductController(ILogger<CatalogController> logger, IPublicCatalogService publicCatalogService)
        : base(logger, publicCatalogService)

    {
    }

    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    [Route("{productId:int}/{*slug}")]
    public async Task<IActionResult> IndexAsync(int productId, string slug)
    { 
        ProductDto product = await this.PublicCatalogService.GetProductAsync(productId);
        return this.SetViewBag().View(product);
    }


}
