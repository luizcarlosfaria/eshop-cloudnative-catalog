
using FluentMigrator;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Architecture.Data;
using TagsAttribute = FluentMigrator.TagsAttribute;
using eShopCloudNative.Catalog.Data;

namespace eShopCloudNative.Catalog.Bootstrapper.Postgres.Migrations;

[Migration(202208_000001)]
[Tags("blue")]
public class Migration00001 : Migration
{
    public override void Up()
    {
        this.Execute.Sql($"CREATE SCHEMA {CatalogConstants.Schema} AUTHORIZATION catalog_user;");

        this.Create
           .Table(nameof(CategoryType), CatalogConstants.Schema)
           .Column<CategoryType>(it => it.CategoryTypeId).PrimaryKey()
           .Column<CategoryType>(it => it.Name, 300).NotNullable()
           .Clob<CategoryType>(it => it.Description).Nullable()
           .Column<CategoryType>(it => it.ShowOnMenu).NotNullable()
           .Column<CategoryType>(it => it.IsHomeShowCase).NotNullable() 
           ;


        this.Create.Sequence("category_seq").InSchema(CatalogConstants.Schema);
        this.Create
           .Table(nameof(Category), CatalogConstants.Schema)
           .Column<Category>(it => it.CategoryId).PrimaryKey()
           .WithColumn($"Parent{nameof(Category.CategoryId)}").AsInt32().Nullable()
               .ForeignKey("FK_CATEGORY_TO_CATEGORY", CatalogConstants.Schema, nameof(Category), nameof(Category.CategoryId))
           .WithColumn(nameof(CategoryType.CategoryTypeId)).AsInt32().NotNullable()
               .ForeignKey("FK_CATEGORYTYPE_TO_CATEGORY", CatalogConstants.Schema, nameof(CategoryType), nameof(CategoryType.CategoryTypeId))
           .Column<Category>(it => it.Name, 300).NotNullable()
           .Clob<Category>(it => it.Description).Nullable()
           .Column<Category>(it => it.Icon, 100).Nullable()
           .Column<Category>(it => it.Slug, 300).NotNullable()
           .Column<Category>(it => it.Active).NotNullable();

        this.Create.Sequence("product_seq").InSchema(CatalogConstants.Schema);
        this.Create
           .Table(nameof(Product), CatalogConstants.Schema)
           .Column<Product>(it => it.ProductId).PrimaryKey()
           .Column<Product>(it => it.Name, 300).NotNullable()
           .Clob<Product>(it => it.Description).Nullable()
           .Column<Product>(it => it.Slug, 300).NotNullable()
           .Column<Product>(it => it.Price, 8, 2).NotNullable()
           .Column<Product>(it => it.Active).NotNullable();

        this.Create.NxNTable<Category, Product>("Product_Category", CatalogConstants.Schema, it => it.CategoryId, it => it.ProductId);

        this.Create
           .Table(nameof(Image), CatalogConstants.Schema)
           .Column<Image>(it => it.ImageId).PrimaryKey()
           .Column<Image>(it => it.FileName, 400).NotNullable()
           .Column<Image>(it => it.Index).NotNullable()
           .WithColumn($"{nameof(Product.ProductId)}").AsInt32().NotNullable()
               .ForeignKey("FK_PRODUCT_TO_IMAGE", CatalogConstants.Schema, nameof(Product), nameof(Product.ProductId))
           ;

    }

    public override void Down()
    {
        this.Delete.Table("Image").InSchema(CatalogConstants.Schema);

        this.Delete.Table("ImageStatus").InSchema(CatalogConstants.Schema);

        this.Delete.Table("Product").InSchema(CatalogConstants.Schema);

        this.Delete.Table("Category").InSchema(CatalogConstants.Schema);

        this.Delete.Schema(CatalogConstants.Schema);
    }

}

