using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
public abstract class EShopControllerBase: Controller
{
    private readonly ILogger logger;

    public EShopControllerBase(ILogger logger)
    {
        this.logger = logger;
    }
}
