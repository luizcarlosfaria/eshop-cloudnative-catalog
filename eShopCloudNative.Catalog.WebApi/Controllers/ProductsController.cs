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

    [HttpGet(Name = "Products")]
    public async Task<IEnumerable<Product>> GetProducts() => await this.productService.GetProducts();
}
