using eShopCloudNative.Catalog.Entities;
using FluentNHibernate.Mapping;

namespace eShopCloudNative.Catalog.Data.Mappings;
public class CategoryTypeMapping : ClassMap<CategoryType>
{
    public CategoryTypeMapping()
    {
        this.DynamicUpdate();
        this.Table(nameof(CategoryType));
        this.Schema(CatalogConstants.Schema);

        this.Id(it => it.CategoryTypeId).GeneratedBy.Sequence("category_seq");
        this.Map(it => it.Name).Length(300).Not.Nullable();
        this.Map(it => it.Description).Nullable().LazyLoad();
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
