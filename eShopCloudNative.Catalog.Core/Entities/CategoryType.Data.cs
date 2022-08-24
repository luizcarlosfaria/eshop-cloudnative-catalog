using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public partial class CategoryType
{
    public virtual int CategoryTypeId { get; set; }

    public virtual string Name { get; set; }
        
    public virtual string Description { get; set; }

    public virtual IList<Category> Categories { get; set; }

    public virtual bool ShowOnMenu { get; set; }

    public virtual bool IsHomeShowCase { get; set; }
}
