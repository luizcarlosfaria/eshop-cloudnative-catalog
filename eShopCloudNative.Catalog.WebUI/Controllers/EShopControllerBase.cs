using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
public abstract class EShopControllerBase: Controller
{
    private readonly ILogger logger;
    internal IPublicCatalogService PublicCatalogService { get; private set; }

    public EShopControllerBase(ILogger logger, IPublicCatalogService publicCatalogService)
    {
        this.PublicCatalogService = publicCatalogService;
        this.logger = logger;
    }

   
}

public static class EShopControllerBaseExtensions
{
    public static T SetViewBag<T>(this T controller)
        where T : EShopControllerBase
    {
        controller.ViewBag.publicCatalogService = controller.PublicCatalogService;
        return controller;
    }
}