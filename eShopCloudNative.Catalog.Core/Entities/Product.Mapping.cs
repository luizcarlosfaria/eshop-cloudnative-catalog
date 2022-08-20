using eShopCloudNative.Catalog.Architecture.Data;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;

public class ProductMapping : NHibernate.Mapping.ByCode.Conformist.ClassMapping<Product>
{
    public ProductMapping()
    {
        //this.Configure("product", Constants.Schema);
        //this.MapSequenceId(it => it.ProductId, "product_id", "product_seq");
        //this.Map(it => it.Name, false, 50, "name");
        //this.MapClob(it => it.Description, false, "description");
        //this.Map(it => it.Slug, false, 300, "slug");
        //this.Map(it => it.Price, false, columnName: "price");
        //this.Map(it => it.Active, false, "active");

    }
}
