using eShopCloudNative.Catalog.Entities;
using FluentNHibernate.Mapping;

namespace eShopCloudNative.Catalog.Data.Mappings;

public class ProductMapping : ClassMap<Product>
{
    public ProductMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(Product));
        this.Schema(CatalogConstants.Schema);

        this.Id(it => it.ProductId).GeneratedBy.Sequence("product_seq");
        this.Map(it => it.Name).Length(300).Not.Nullable();
        this.Map(it => it.Description).Length(8000).Nullable().LazyLoad();
        this.Map(it => it.Slug).Length(300).Not.Nullable();
        this.Map(it => it.Price).Precision(8).Scale(2).Not.Nullable();
        this.Map(it => it.Active).Not.Nullable();

        this.HasManyToMany(it => it.Categories)
            //.ParentKeyColumns.Add(nameof(Product.ProductId), p => p.UniqueKey("pk_product_category"))
            .ParentKeyColumns.Add(nameof(Product.ProductId))
            .Table("Product_Category")
            //.ChildKeyColumns.Add(nameof(Category.CategoryId), p => p.UniqueKey("pk_product_category"))
            .ChildKeyColumns.Add(nameof(Category.CategoryId))
            .ForeignKeyConstraintNames("fk_product_to_product_category", "fk_category_to_product_category")
            .LazyLoad()
            .Fetch.Select()
            .AsBag();

        this.HasMany(it => it.Images)
            .KeyColumns.Add(nameof(Product.ProductId))
            .Inverse()
            .Cascade.Delete()
            .LazyLoad()
            .Fetch.Select()
            .AsList();
    }
}
