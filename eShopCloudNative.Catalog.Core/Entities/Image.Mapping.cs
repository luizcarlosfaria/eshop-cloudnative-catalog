using eShopCloudNative.Catalog.Architecture.Data;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;

public class ImageMapping : ClassMap<Image>
{
    public ImageMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(Image));
        this.Schema(Constants.Schema);

        this.Id(it => it.ImageId).GeneratedBy.Assigned();
        this.Map(it => it.FileName).Length(400).Not.Nullable();
        this.Map(it => it.Index).Not.Nullable();

        this.References(it => it.Product)
            .Column(nameof(Product.ProductId))
            .Not.Nullable()
            .Fetch.Join()
            .Cascade.None();
    }
}
