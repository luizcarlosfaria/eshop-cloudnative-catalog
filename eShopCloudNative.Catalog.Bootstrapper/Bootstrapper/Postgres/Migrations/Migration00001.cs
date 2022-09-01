
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
           .Column<CategoryType>(it => it.CategoryTypeId).PrimaryKey()
           .Column<CategoryType>(it => it.Name, 300).NotNullable()
           .Column<CategoryType>(it => it.Description, 8000).Nullable()
           .Column<CategoryType>(it => it.ShowOnMenu).NotNullable()
           .Column<CategoryType>(it => it.IsHomeShowCase).NotNullable() 
           ;


        this.Create.Sequence("category_seq").InSchema(Constants.Schema);
        this.Create
           .Table(nameof(Category), Constants.Schema)
           .Column<Category>(it => it.CategoryId).PrimaryKey()
           .WithColumn($"Parent{nameof(Category.CategoryId)}").AsInt32().Nullable()
               .ForeignKey("FK_CATEGORY_TO_CATEGORY", Constants.Schema, nameof(Category), nameof(Category.CategoryId))
           .WithColumn(nameof(CategoryType.CategoryTypeId)).AsInt32().NotNullable()
               .ForeignKey("FK_CATEGORYTYPE_TO_CATEGORY", Constants.Schema, nameof(CategoryType), nameof(CategoryType.CategoryTypeId))
           .Column<Category>(it => it.Name, 300).NotNullable()
           .Column<Category>(it => it.Description, 8000).Nullable()
           .Column<Category>(it => it.Icon, 100).Nullable()
           .Column<Category>(it => it.Slug, 300).NotNullable()
           .Column<Category>(it => it.Active).NotNullable();

        this.Create.Sequence("product_seq").InSchema(Constants.Schema);
        this.Create
           .Table(nameof(Product), Constants.Schema)
           .Column<Product>(it => it.ProductId).PrimaryKey()
           .Column<Product>(it => it.Name, 300).NotNullable()
           .Column<Product>(it => it.Description, 8000).Nullable()
           .Column<Product>(it => it.Slug, 300).NotNullable()
           .Column<Product>(it => it.Price, 8, 2).NotNullable()
           .Column<Product>(it => it.Active).NotNullable();

        this.Create.NxNTable<Category, Product>("Product_Category", Constants.Schema, it => it.CategoryId, it => it.ProductId);

        this.Create
           .Table(nameof(Image), Constants.Schema)
           .Column<Image>(it => it.ImageId).PrimaryKey()
           .Column<Image>(it => it.FileName, 400).NotNullable()
           .WithColumn($"{nameof(Product.ProductId)}").AsInt32().NotNullable()
               .ForeignKey("FK_PRODUCT_TO_IMAGE", Constants.Schema, nameof(Product), nameof(Product.ProductId))
           ;

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

