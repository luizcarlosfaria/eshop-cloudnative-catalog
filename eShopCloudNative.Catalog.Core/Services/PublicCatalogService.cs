using AutoMapper;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class PublicCatalogService : BaseService, IPublicCatalogService
{
    private readonly CategoryQueryRepository categoryQuery;

    public PublicCatalogService(IMapper mapper, CategoryQueryRepository categoryQuery): base(mapper)
    {
        this.categoryQuery = categoryQuery;
    }

    public Task<IEnumerable<CategoryDto>> GetCategoriesForMenu()
        => this.ExecuteAndAdapt<CategoryDto, Category>(() => this.categoryQuery.GetCategoriesForMenu());

    public Task<IEnumerable<CategoryDto>> GetHomeCatalog()
        => this.ExecuteAndAdapt<CategoryDto, Category>(() => this.categoryQuery.GetHomeCatalog());

}
