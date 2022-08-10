using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
public class ImageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
