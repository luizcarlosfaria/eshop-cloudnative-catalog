using AutoMapper;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class PublicCatalogService : IPublicCatalogService
{
    private readonly IMapper mapper;
    private readonly CategoryQueryRepository categoryQuery;

    public PublicCatalogService(IMapper mapper, CategoryQueryRepository categoryQuery)
    {
        this.mapper = mapper;
        this.categoryQuery = categoryQuery;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesForMenu()
    {
        var dbResult = await categoryQuery.GetCategoriesForMenu();

        var apiResult = mapper.Map<IEnumerable<CategoryDto>>(dbResult);

        return apiResult;
    }

    public async Task<IEnumerable<CategoryDto>> GetHomeCatalog()
    {
        var dbResult = await categoryQuery.GetHomeCatalog();

        var apiResult = mapper.Map<IEnumerable<CategoryDto>>(dbResult);

        return apiResult;
    }

}
