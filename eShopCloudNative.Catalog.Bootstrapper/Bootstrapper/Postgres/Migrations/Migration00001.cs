using FluentMigrator;
using FluentMigrator.Postgres;
using FluentMigrator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Entities;
using eShopCloudNative.Catalog.Architecture;

namespace eShopCloudNative.Catalog.Bootstrapper.Postgres.Migrations;

[Migration(202208_000001)]
[Tags("blue")]
public class Migration00001 : Migration
{
    public override void Up()
    {
        this.Execute.Sql($"CREATE SCHEMA {Constants.Schema} AUTHORIZATION catalog_user;");

        this.Create
           .Table(nameof(CategoryType), Constants.Schema)
           .Map<CategoryType>(it => it.CategoryTypeId).PrimaryKey()
           .Map<CategoryType>(it => it.Name, 300).NotNullable()
           .Map<CategoryType>(it => it.Description, 8000).Nullable()
           .Map<CategoryType>(it => it.ShowOnMenu).NotNullable()
           .Map<CategoryType>(it => it.IsHomeShowCase).NotNullable() 
           ;


        this.Create.Sequence("category_seq").InSchema(Constants.Schema);
        this.Create
           .Table(nameof(Category), Constants.Schema)
           .Map<Category>(it => it.CategoryId).PrimaryKey()
           .WithColumn($"Parent{nameof(Category.CategoryId)}").AsInt32().Nullable()
               .ForeignKey("FK_CATEGORY_TO_CATEGORY", Constants.Schema, nameof(Category), nameof(Category.CategoryId))
           .WithColumn(nameof(CategoryType.CategoryTypeId)).AsInt32().NotNullable()
               .ForeignKey("FK_CATEGORYTYPE_TO_CATEGORY", Constants.Schema, nameof(CategoryType), nameof(CategoryType.CategoryTypeId))
           .Map<Category>(it => it.Name, 300).NotNullable()
           .Map<Category>(it => it.Description, 8000).Nullable()
           .Map<Category>(it => it.Icon, 100).Nullable()
           .Map<Category>(it => it.Slug, 300).NotNullable()
           .Map<Category>(it => it.Active).NotNullable();

        this.Create.Sequence("product_seq").InSchema(Constants.Schema);
        this.Create
           .Table(nameof(Product), Constants.Schema)
           .Map<Product>(it => it.ProductId).PrimaryKey()
           .Map<Product>(it => it.Name, 300).NotNullable()
           .Map<Product>(it => it.Description, 8000).Nullable()
           .Map<Product>(it => it.Slug, 300).NotNullable()
           .Map<Product>(it => it.Price, 8, 2).NotNullable()
           .Map<Product>(it => it.Active).NotNullable();

        this.Create
           .Table("Product_Category", Constants.Schema)
           .WithColumn(nameof(Category.CategoryId)).AsInt32().Nullable()
               .ForeignKey("FK_CATEGORY_TO_PRODUCT_CATEGORY", Constants.Schema, nameof(Category), nameof(Category.CategoryId))
           .WithColumn(nameof(Product.ProductId)).AsInt32().Nullable()
               .ForeignKey("FK_PRODUCT_TO_PRODUCT_CATEGORY", Constants.Schema, nameof(Product), nameof(Product.ProductId));


        //this.Create
        //   .Table("ImageStatus").InSchema(Constants.Schema)
        //   .WithColumn("ImageStatusId").AsInt32().PrimaryKey()
        //   .WithColumn("Name").AsString(300).NotNullable()
        //   .WithColumn("Description").AsString(8000).Nullable()
        //   .WithColumn("EnableOnCatalog").AsBoolean().NotNullable();

        //this.Create
        //   .Table("Image").InSchema(Constants.Schema)
        //   .WithColumn("ImageId").AsInt32().PrimaryKey().Identity()
        //   .WithColumn("ProductId").AsInt32().Nullable()
        //       .ForeignKey("FK_Product_TO_Images", Constants.Schema, "Product", "ProductId")
        //   .WithColumn("ImageStatusId").AsInt32().Nullable()
        //       .ForeignKey("FK_ImageStatus_TO_Images", Constants.Schema, "ImageStatus", "ImageStatusId")
        //   .WithColumn("Bucket").AsString(300).NotNullable()
        //   .WithColumn("Name").AsString(300).NotNullable()
        //   .WithColumn("Active").AsBoolean().NotNullable();



    }

    public override void Down()
    {
        this.Delete.Table("Image").InSchema(Constants.Schema);

        this.Delete.Table("ImageStatus").InSchema(Constants.Schema);

        this.Delete.Table("Product").InSchema(Constants.Schema);

        this.Delete.Table("Category").InSchema(Constants.Schema);

        this.Delete.Schema(Constants.Schema);
    }

}

