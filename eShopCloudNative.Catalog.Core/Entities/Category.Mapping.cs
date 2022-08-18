using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopCloudNative.Catalog.Architecture.Data;
using NHibernate.Mapping.ByCode;

namespace eShopCloudNative.Catalog.Entities;
public class CategoryMapping : NHibernate.Mapping.ByCode.Conformist.ClassMapping<Category>
{
    public CategoryMapping()
    {
        this.Configure("category", Constants.Schema);
        this.MapSequenceId(it => it.CategoryId, "category_id", "category_seq");
        this.Map(it => it.Name, false, 50, "name");
        this.MapClob(it => it.Description, false, "description");
        this.Map(it => it.Slug, false, 300, "slug");
        this.Map(it => it.Active, false, "active");
        //this.MapManyToOne(it => it.Parent, true, "FK_Category_TO_Category", "ParentCategoryId");
        //this.MapOneToMany(it => it.Children, it => it.CategoryId, "CategoryId");
        //this.MapOneToMany(it => it.Products, it => it.CategoryId, "CategoryId");
    }
}
