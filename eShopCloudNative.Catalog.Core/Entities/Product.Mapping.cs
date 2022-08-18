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
        this.Configure("product", Constants.Schema);
        this.MapSequenceId(it => it.ProductId, nameof(Product.ProductId), "product_seq");
        this.Map(it => it.Name, false, 50);
        this.MapClob(it => it.Description, false);
        this.Map(it => it.Slug, false, 300);
        this.Map(it => it.Price, false);
        this.Map(it => it.Active, false);

    }
}
