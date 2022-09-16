using AutoMapper;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;



namespace eShopCloudNative.Catalog.Services;
public class PublicCatalogService : BaseService, IPublicCatalogService
{
    private readonly ICategoryQueryRepository categoryQuery;

    public PublicCatalogService(IMapper mapper, ICategoryQueryRepository categoryQuery)
        : base(mapper)
    {
        this.categoryQuery = categoryQuery;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesForMenu()
        => await this.ExecuteAndAdapt<CategoryDto, Category>(async ()
            => await this.categoryQuery.GetCategoriesForMenu());

    public async Task<IEnumerable<CategoryDto>> GetHomeCatalog()
        => await this.ExecuteAndAdapt<CategoryDto, Category>(async ()
            => await this.categoryQuery.GetHomeCatalog());

}
