using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopCloudNative.Catalog.Architecture.Data;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;

namespace eShopCloudNative.Catalog.Entities;
public class CategoryMapping : ClassMap<Category>
{
    public CategoryMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(Category));
        this.Schema(Constants.Schema);

        this.Id(it => it.CategoryId).GeneratedBy.Sequence("category_seq");
        this.Map(it => it.Name).Length(300).Not.Nullable();
        this.Map(it => it.Icon).Length(100).Nullable();
        this.Map(it => it.Description).Length(8000).Nullable().LazyLoad();
        this.Map(it => it.Slug).Length(300).Not.Nullable();
        this.Map(it => it.Active).Not.Nullable();
        this.HasMany(it => it.Children)
            .KeyColumns.Add($"Parent{nameof(Category.CategoryId)}")
            .Inverse()
            .Cascade.Delete()
            .LazyLoad()
            .Fetch.Select()
            .AsBag();

        this.References(it => it.Parent)
            .Column($"Parent{nameof(Category.CategoryId)}")
            .Nullable()
            .LazyLoad()
            .Fetch.Select()
            .Cascade.None();

        this.References(it => it.Type)
            .Column(nameof(CategoryType.CategoryTypeId))
            .Not.Nullable()
            .Fetch.Join()
            .Cascade.None();

        this.HasManyToMany(it => it.Products)
            .ParentKeyColumns.Add(nameof(Category.CategoryId), p => p.UniqueKey("pk_product_category"))
            .Table("Product_Category")
            .ChildKeyColumns.Add(nameof(Product.ProductId), p => p.UniqueKey("pk_product_category"))
            .ForeignKeyConstraintNames("fk_category_to_product_category", "fk_product_to_product_category")
            .LazyLoad()
            .Fetch.Select()
            .AsBag();
        
    }
}
