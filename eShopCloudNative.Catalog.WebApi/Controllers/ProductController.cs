using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase, IProductService
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

}
