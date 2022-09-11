using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("Public/Catalog")]
public class PublicCatalogController : ControllerBase, IPublicCatalogService
{
    private readonly IPublicCatalogService categoryService;

    public PublicCatalogController(IPublicCatalogService  categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet("CategoriesForMenu", Name = "CategoriesForMenu")]
    public  Task<IEnumerable<CategoryDto>> GetCategoriesForMenu() 
        => this.categoryService.GetCategoriesForMenu();

    [HttpGet("HomeCatalog", Name = "HomeCatalog")]
    public Task<IEnumerable<CategoryDto>> GetHomeCatalog() 
        => this.categoryService.GetHomeCatalog();
}
