using eShopCloudNative.Catalog.Services;
using Minio;
using Refit;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Core;
using eShopCloudNative.Architecture.Logging;
using eShopCloudNative.Catalog;
using eShopCloudNative.Architecture.Extensions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureWith("WebConstants", WebConstants.Instance);

builder.AddEnterpriseApplicationLog("Enterprise:Application:Log", Mode.Integrated);

EnterpriseApplicationLog.SetGlobalContext("eShopCloudNative.Catalog.WebUI");

builder.Services.AddControllersWithViews();

builder.Services.AddResponseCaching();

builder.Services
    .AddRefitClient<IPublicCatalogService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri($"{builder.Configuration.GetValue<string>("eshop-cloudnative:global:api-gateway")}/catalog");
        c.DefaultRequestHeaders.Add("apikey", builder.Configuration.GetValue<string>("eshop-cloudnative:global:apikey"));
        //TODO: Adicionar versão atual!
    });


//######
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

})
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/perfil/entrar");
                options.LogoutPath = new PathString("/perfil/sair");
                options.AccessDeniedPath = new PathString("/perfil/acesso-negado");
            })
            .AddJwtBearer("Bearer", options =>
            {
                builder.Configuration.Bind("bearer", options);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };

            })
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                builder.Configuration.Bind("oidc", options);

                options.ClaimActions.MapAll();

                //TODO: Falha de Segurança para uso em localhost
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                };

                options.Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = context =>
                    {
                        context.Response.Redirect("/");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    },
                    OnRedirectToIdentityProvider = context =>
                    {
                        //context.ProtocolMessage.RedirectUri = $"{this.Configuration.GetValue<string>("applicationUrl")}/signin-oidc";
                        return Task.CompletedTask;
                    },
                };

            });

//#####

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Run();


