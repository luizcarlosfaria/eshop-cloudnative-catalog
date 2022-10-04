using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopCloudNative.Catalog.Controllers;
[ApiController]
[Route("Ping")]
public partial class PingController : ControllerBase
{
    public PingController()
    {
    }

    [HttpGet(Name = "Ping")]
    public IActionResult Ping() => this.Ok();

}
