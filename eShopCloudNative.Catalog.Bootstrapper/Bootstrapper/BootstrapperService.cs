using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper;
public class BootstrapperService : IBootstrapperService
{    

    public List<IBootstrapperService> Services { get; set; }

    public async Task InitializeAsync(IConfiguration configuration)
    {
        foreach (var service in this.Services)
        {
            await service.InitializeAsync(configuration);
        }
    }

    public async Task ExecuteAsync(IConfiguration configuration)
    {
        foreach (var service in this.Services)
        {
            await service.ExecuteAsync(configuration);
        }
    }

}
