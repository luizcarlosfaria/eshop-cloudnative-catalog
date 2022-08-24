using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopCloudNative.Catalog.Architecture.Data;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;

namespace eShopCloudNative.Catalog.Entities;
public class CategoryTypeMapping : ClassMap<CategoryType>
{
    public CategoryTypeMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(CategoryType));
        this.Schema(Constants.Schema);

        this.Id(it => it.CategoryTypeId).GeneratedBy.Sequence("category_seq");
        this.Map(it => it.Name).Length(300).Not.Nullable();
        this.Map(it => it.Description).Length(8000).Nullable().LazyLoad();
        this.Map(it => it.ShowOnMenu).Not.Nullable();
        this.Map(it => it.IsHomeShowCase).Not.Nullable();
        this.HasMany(it => it.Categories)
            .KeyColumns.Add(nameof(CategoryType.CategoryTypeId))
            .Inverse()
            .Cascade.Delete()
            .LazyLoad()
            .Fetch.Select()
            .AsBag();


        //this.MapManyToOne(it => it.Parent, true, "FK_Category_TO_Category", "ParentCategoryId");
        //this.MapOneToMany(it => it.Children, it => it.CategoryId, "CategoryId");
        //this.MapOneToMany(it => it.Products, it => it.CategoryId, "CategoryId");
    }
}
