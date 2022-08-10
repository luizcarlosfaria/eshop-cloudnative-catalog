using eShopCloudNative.Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;
public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        this._logger = logger;
    }

    public IActionResult Index()
    {
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
