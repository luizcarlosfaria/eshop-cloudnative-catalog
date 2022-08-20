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
        this.Table("category");
        this.Schema(Constants.Schema);

        this.Id(it => it.CategoryId, "category_id").GeneratedBy.Sequence("category_seq");

        this.Map(it => it.Name, "name").Length(300).Not.Nullable();

        this.Map(it => it.Description, "description").Length(8000).Nullable().LazyLoad();

        this.Map(it => it.Slug, "slug").Length(300).Not.Nullable();

        this.Map(it => it.Active, "active").Not.Nullable();

        this.HasMany(it => it.Children)
            .KeyColumns.Add("category_id")
            .Inverse()
            .Cascade.Delete()
            .LazyLoad()
            .Fetch.Select()
            .AsBag();

        this.References(it => it.Parent)
            .Column("parent_category_id")
            .Nullable()
            .Fetch.Join()
            .Cascade.None();

        this.HasManyToMany(it => it.Products)
            .ParentKeyColumns.Add("category_id", p => p.UniqueKey("pk_product_category"))
            .Table("product_category")
            .ChildKeyColumns.Add("product_id", p => p.UniqueKey("pk_product_category"))
            .ForeignKeyConstraintNames("fk_category_to_product_category", "fk_product_to_product_category")
            .LazyLoad()
            .Fetch.Select()
            .AsBag();

        //this.MapManyToOne(it => it.Parent, true, "FK_Category_TO_Category", "ParentCategoryId");
        //this.MapOneToMany(it => it.Children, it => it.CategoryId, "CategoryId");
        //this.MapOneToMany(it => it.Products, it => it.CategoryId, "CategoryId");
    }
}
