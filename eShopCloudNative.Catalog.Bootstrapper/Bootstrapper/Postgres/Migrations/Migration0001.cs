using FluentMigrator;
using FluentMigrator.Postgres;
using FluentMigrator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper.Postgres.Migrations;

[Migration(2022080500001)]
public class Migration0001 : Migration
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
            .WithColumn("Active").AsBoolean().NotNullable();

        
    }

    public override void Down()
    {
        this.Delete.Table("Category").InSchema("Catalog");
    }

}

