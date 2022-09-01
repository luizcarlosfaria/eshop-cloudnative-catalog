using AutoMapper;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService : IProductService
{
    private readonly IMapper mapper;
    private readonly CategoryQueryRepository categoryQuery;

    public ProductService(IMapper mapper, CategoryQueryRepository categoryQuery)
    {
        this.mapper = mapper;
        this.categoryQuery = categoryQuery;
    }

    public async Task<IEnumerable<CategoryDto>> GetHomeCatalog()
    {
        var dbResult = await categoryQuery.GetShowCaseCategoriesWithProducts();


        var apiResult = mapper.Map<IEnumerable<CategoryDto>>(dbResult);

        return apiResult;
    }

}
