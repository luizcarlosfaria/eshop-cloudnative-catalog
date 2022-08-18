using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public partial class Product
{
    public virtual int ProductId { get; set; }
    public virtual string? Name { get; set; }
    public virtual List<Category>? Categories { get; set; }
    public virtual string? Description { get; set; }
    public virtual string? Slug { get; set; }
    public virtual decimal Price { get; set; }
    public virtual bool Active { get; set; }
}
