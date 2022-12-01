using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;

[Route("/p")]
public class ProductController : EShopControllerBase
{
    private readonly IPublicCatalogService publicCatalogService;

    public ProductController(ILogger<CatalogController> logger, IPublicCatalogService publicCatalogService)
        : base(logger)

    {
        this.publicCatalogService = publicCatalogService;
    }

    //[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = false)]
    [Route("{productId:int}/{slug}")]
    public async Task<IActionResult> IndexAsync(int productId, string slug)
    {
        ProductDto product = await this.publicCatalogService.GetProductAsync(productId);
        return this.View(product);
    }

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("{productId:int}/{slug}/price")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetPriceIndexAsync(int productId, string slug)
    {
        if(slug.Contains("brasileira-azul") ) {
            return this.BadRequest("Fake, exemplo de falha");
        }

        decimal price = await this.publicCatalogService.GetProductPriceAsync(productId);

        return this.Content($"R$ {price}");
    }
}
