using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper;
public class BootstrapperService: IBootstrapperService
{
    public List<IBootstrapperService> Services { get; set; }

    public void Initialize()
    {
        foreach (var service in this.Services)
        {
            service.Initialize();
        }
    }

    public void Execute()
    {
        foreach (var service in this.Services)
        {
            service.Execute();
        }
    }



}
