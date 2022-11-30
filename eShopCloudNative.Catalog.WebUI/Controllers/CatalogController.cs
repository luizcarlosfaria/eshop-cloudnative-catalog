using eShopCloudNative.Catalog.Models;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Controllers;
public class CatalogController : EShopControllerBase
{
    public CatalogController(ILogger<CatalogController> logger)
    : base(logger)

    {
    }

    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IActionResult Index()
        => this.View();

    public IActionResult Privacy() 
        => this.View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
}
