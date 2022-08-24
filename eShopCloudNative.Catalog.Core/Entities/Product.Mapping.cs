using eShopCloudNative.Catalog.Architecture.Data;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;

public class ProductMapping : ClassMap<Product>
{
    public ProductMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(Product));
        this.Schema(Constants.Schema);

        this.Id(it => it.ProductId).GeneratedBy.Sequence("product_seq");
        this.Map(it => it.Name).Length(300).Not.Nullable();
        this.Map(it => it.Description).Length(8000).Nullable().LazyLoad();
        this.Map(it => it.Slug).Length(300).Not.Nullable();
        this.Map(it => it.Price).Precision(8).Scale(2).Not.Nullable();
        this.Map(it => it.Active).Not.Nullable();

        this.HasManyToMany(it => it.Categories)
        .ParentKeyColumns.Add(nameof(Product.ProductId), p => p.UniqueKey("pk_product_category"))
        .Table("Product_Category")
        .ChildKeyColumns.Add(nameof(Category.CategoryId), p => p.UniqueKey("pk_product_category"))
        .ForeignKeyConstraintNames("fk_product_to_product_category", "fk_category_to_product_category")
        .LazyLoad()
        .Fetch.Select()
        .AsBag();
    }
}
