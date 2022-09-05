using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;
public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> logger;
    private readonly IPublicCatalogService categoryService;

    public CatalogController(ILogger<CatalogController> logger, IPublicCatalogService categoryService)
    {
        this.logger = logger;
        this.categoryService = categoryService;
    }

    private CatalogController SetViewBag() {
        this.ViewBag.categoryService = categoryService;
        return this;
    }

    public IActionResult Index() => this.SetViewBag().View();

    public IActionResult Privacy() => this.View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
