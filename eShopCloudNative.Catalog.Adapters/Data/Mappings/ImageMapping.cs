using eShopCloudNative.Catalog.Entities;
using FluentNHibernate.Mapping;

namespace eShopCloudNative.Catalog.Data.Mappings;

public class ImageMapping : ClassMap<Image>
{
    public ImageMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(Image));
        this.Schema(CatalogConstants.Schema);

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
