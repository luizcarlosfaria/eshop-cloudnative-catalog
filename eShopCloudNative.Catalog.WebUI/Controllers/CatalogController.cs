using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;
public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;
    private readonly IProductService productService;

    public CatalogController(ILogger<CatalogController> logger, IProductService productService)
    {
        this._logger = logger;
        this.productService = productService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var model = await this.productService.GetProducts();

        return this.View();
    }

    public IActionResult Privacy()
    {
        return this.View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
