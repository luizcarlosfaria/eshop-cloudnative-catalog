using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase, ICategoryService
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService  categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet("HomeCatalog", Name = "HomeCatalog")]
    public async Task<IEnumerable<CategoryDto>> GetHomeCatalog() => await this.categoryService.GetHomeCatalog();
}
