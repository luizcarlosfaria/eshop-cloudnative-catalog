using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHome")]
    public IEnumerable<ProductDto> Get([FromServices] IProductService svc) => svc.GetProducts();
    
}
