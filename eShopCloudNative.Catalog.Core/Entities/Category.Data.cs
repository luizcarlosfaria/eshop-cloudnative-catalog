using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public partial class Category
{
    public virtual int CategoryId { get; set; }
    public virtual string Name { get; set; }

    public virtual Category Parent { get; set; }

    public virtual CategoryType Type { get; set; }

    public virtual IList<Category> Children { get; set; }

    public virtual IList<Product> Products { get; set; }

    public virtual string Icon { get; set; }

    public virtual string Description { get; set; }

    public virtual string Slug { get; set; }

    public virtual bool Active { get; set; }
    
}
