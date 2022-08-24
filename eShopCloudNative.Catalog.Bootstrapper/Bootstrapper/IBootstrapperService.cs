using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper;

public interface IBootstrapperService
{
    Task InitializeAsync(IConfiguration configuration);

    Task ExecuteAsync(IConfiguration configuration);

}
