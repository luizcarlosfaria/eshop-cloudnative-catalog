using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public partial class Category
{
    public virtual int CategoryId { get; set; }
    public virtual string? Name { get; set; }

    public virtual Category? Parent { get; set; }
    public virtual List<Category>? Children { get; set; }

    public virtual List<Product>? Products { get; set; }

    public virtual string? Description { get; set; }
    public virtual string? Slug { get; set; }
    public virtual bool Active { get; set; }
}
