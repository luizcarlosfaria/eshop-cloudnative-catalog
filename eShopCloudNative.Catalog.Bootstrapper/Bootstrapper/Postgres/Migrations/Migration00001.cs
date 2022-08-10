using FluentMigrator;
using FluentMigrator.Postgres;
using FluentMigrator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper.Postgres.Migrations;

[Migration(202208_000001)]
public class Migration00001 : Migration
{
    public override void Up()
    {
        this.Create.Schema("Catalog");

        this.Create
           .Table("Category").InSchema("Catalog")
           .WithColumn("CategoryId").AsInt32().PrimaryKey().Identity()
           .WithColumn("ParentCategoryId").AsInt32().Nullable()
               .ForeignKey("FK_Category_TO_Category", "Catalog", "Category", "CategoryId")
           .WithColumn("Name").AsString(50).NotNullable()
           .WithColumn("Slug").AsString(300).NotNullable()
           .WithColumn("Active").AsBoolean().NotNullable();

        this.Create
           .Table("Product").InSchema("Catalog")
           .WithColumn("ProductId").AsInt32().PrimaryKey().Identity()
           .WithColumn("CategoryId").AsInt32().Nullable()
               .ForeignKey("FK_Category_TO_Product", "Catalog", "Category", "CategoryId")
           .WithColumn("Name").AsString(300).NotNullable()
           .WithColumn("Description").AsString(8000).Nullable()
           .WithColumn("Slug").AsString(300).NotNullable()
           .WithColumn("Active").AsBoolean().NotNullable();

        this.Create
           .Table("Product_Category").InSchema("Catalog")
           .WithColumn("ProductId").AsInt32().PrimaryKey().NotNullable()
                .ForeignKey("FK_Product_TO_Product_Category", "Catalog", "Product", "ProductId")
           .WithColumn("CategoryId").AsInt32().PrimaryKey().NotNullable()
               .ForeignKey("FK_Category_TO_Product_Category", "Catalog", "Category", "CategoryId");
          

    }

    public override void Down()
    {
        this.Delete.Table("Product_Category").InSchema("Catalog");

        this.Delete.Table("Product").InSchema("Catalog");

        this.Delete.Table("Category").InSchema("Catalog");

        this.Delete.Schema("Catalog");
    }

}

