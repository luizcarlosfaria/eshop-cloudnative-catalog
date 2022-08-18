using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase, IProductService
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet("HomeCatalog", Name = "HomeCatalog")]
    public async Task<IEnumerable<ProductDto>> GetHomeCatalog() => await this.productService.GetHomeCatalog();
}
