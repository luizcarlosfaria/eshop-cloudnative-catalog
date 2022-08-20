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
        this.Table("product");
        this.Schema(Constants.Schema);

        this.Id(it => it.ProductId, "product_id").GeneratedBy.Sequence("product_seq");

        this.Map(it => it.Name, "name").Length(300).Not.Nullable();

        this.Map(it => it.Description, "description").Length(8000).Nullable().LazyLoad();

        this.Map(it => it.Slug, "slug").Length(300).Not.Nullable();

        this.Map(it => it.Price, "price").Precision(8).Scale(2).Not.Nullable();

        this.Map(it => it.Active, "active").Not.Nullable();

        this.HasManyToMany(it => it.Categories)
        .ParentKeyColumns.Add("product_id", p => p.UniqueKey("pk_product_category"))
        .Table("product_category")
        .ChildKeyColumns.Add("category_id", p => p.UniqueKey("pk_product_category"))
        .ForeignKeyConstraintNames("fk_product_to_product_category", "fk_category_to_product_category")
        .LazyLoad()
        .Fetch.Select()
        .AsBag();
    }
}
