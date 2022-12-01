using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;

[Route("/c")]
public class CategoryController : EShopControllerBase
{
    private readonly IPublicCatalogService publicCatalogService;

    public CategoryController(ILogger<CatalogController> logger, IPublicCatalogService publicCatalogService)
    : base(logger)

    {
        this.publicCatalogService = publicCatalogService;
    }

    //[ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    [Route("{categoryId:int}/{*slug}")]
    public async Task<IActionResult> IndexAsync(int categoryId, string slug)
        => this.View("Shared.Category", await this.publicCatalogService.GetCategoryAsync(categoryId));

}
