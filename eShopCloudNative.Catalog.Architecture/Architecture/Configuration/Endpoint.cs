using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Architecture.Configuration;
public class Endpoint
{
    public string Host { get; set; }

    public int Port { get; set; }

    public bool Encrypted{ get; set; }
}


public class Credential
{ 
    public string UserName { get; set; }

    public string Password { get; set; }
}