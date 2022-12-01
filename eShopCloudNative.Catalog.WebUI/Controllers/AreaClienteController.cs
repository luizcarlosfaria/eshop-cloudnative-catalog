using eShopCloudNative.Catalog.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace eShopCloudNative.Catalog.Controllers;

[Route("/area-cliente")]
public class AreaClienteController : EShopControllerBase
{
    public AreaClienteController(ILogger<AreaClienteController> logger) : base(logger)
    {
    }

    [Authorize()]
    public IActionResult Index()
    {
        return this.View();
    }

    [HttpGet("sair")]
    public async Task<IActionResult> Sair([FromServices] IConfiguration configuration)
    {
        await this.HttpContext.SignOutAsync("Cookies");

        await this.HttpContext.SignOutAsync("OpenIdConnect");

        string path = UrlEncoder.Default.Encode($"{this.Request.Scheme}://{this.Request.Host.Value}/");

        //Keycloak
        //return this.Redirect($"{configuration.GetValue<string>("oidc:Authority")}/protocol/openid-connect/logout?redirect_uri={path}"); //?=encodedRedirectUri

        //Identity
        return this.Redirect($"{configuration.GetValue<string>("oidc:Authority")}/account/forcelogout?redirect_uri={path}"); //?=encodedRedirectUri
    }
}
