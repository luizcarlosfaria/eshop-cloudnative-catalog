using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public partial class Image
{
    public virtual Guid ImageId { get; set; }    
    public virtual Product Product { get; set; }
    public virtual string FileName { get; set; }
}



