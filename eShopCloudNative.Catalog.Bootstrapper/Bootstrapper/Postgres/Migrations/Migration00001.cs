using FluentMigrator;
using FluentMigrator.Postgres;
using FluentMigrator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopCloudNative.Catalog.Architecture.Data;

namespace eShopCloudNative.Catalog.Bootstrapper.Postgres.Migrations;

[Migration(202208_000001)]
[Tags("blue")]
public class Migration00001 : Migration
{
    public override void Up()
    {
        this.Execute.Sql($"CREATE SCHEMA {Constants.Schema} AUTHORIZATION catalog_user;");

       

        this.Create.Sequence("category_seq").InSchema(Constants.Schema);
        this.Create
           .Table("category").InSchema(Constants.Schema)
           .WithColumn("category_id").AsInt32().PrimaryKey()
           .WithColumn("parent_category_id").AsInt32().Nullable()
               .ForeignKey("fk_category_to_category", Constants.Schema, "category", "category_id")
           .WithColumn("name"). AsString(300).NotNullable()
           .WithColumn("description").AsString(8000).Nullable()
           .WithColumn("slug").AsString(300).NotNullable()
           .WithColumn("active").AsBoolean().NotNullable();

        this.Create.Sequence("product_seq").InSchema(Constants.Schema);
        this.Create
           .Table("product").InSchema(Constants.Schema)
           .WithColumn("product_id").AsInt32().PrimaryKey()
           .WithColumn("name").AsString(300).NotNullable()
           .WithColumn("description").AsString(8000).Nullable()
           .WithColumn("slug").AsString(300).NotNullable()
           .WithColumn("price").AsDecimal(8,2).NotNullable()
           .WithColumn("active").AsBoolean().NotNullable();

        this.Create
           .Table("product_category")
           .InSchema(Constants.Schema)
           .WithColumn("category_id").AsInt32().Nullable()
               .ForeignKey("fk_category_to_product_category", Constants.Schema, "category", "category_id")
           .WithColumn("product_id").AsInt32().Nullable()
               .ForeignKey("fk_product_to_product_category", Constants.Schema, "product", "product_id");


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

