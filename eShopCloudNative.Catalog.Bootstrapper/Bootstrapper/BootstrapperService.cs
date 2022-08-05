using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper;
public class BootstrapperService: IBootstrapperService
{
    public List<IBootstrapperService> Services { get; set; }

    public async Task InitializeAsync()
    {
        foreach (var service in this.Services)
        {
            await service.InitializeAsync();
        }
    }

    public async Task ExecuteAsync()
    {
        foreach (var service in this.Services)
        {
            await service.ExecuteAsync();
        }
    }



}
