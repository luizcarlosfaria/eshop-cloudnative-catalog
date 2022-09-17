using AutoMapper;
using eShopCloudNative.Catalog.Data.Repositories;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;



namespace eShopCloudNative.Catalog.Services;
public class PublicCatalogService : BaseService, IPublicCatalogService
{
    private readonly ICategoryQueryRepository categoryQuery;
    private readonly IProductQueryRepository productQuery;

    public PublicCatalogService(IMapper mapper, ICategoryQueryRepository categoryQuery, IProductQueryRepository productQuery)
        : base(mapper)
    {
        this.categoryQuery = categoryQuery;
        this.productQuery = productQuery;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesForMenuAsync()
        => await this.ExecuteAndAdaptAsync<CategoryDto, Category>(async ()
            => await this.categoryQuery.GetCategoriesForMenu());

    public async Task<IEnumerable<CategoryDto>> GetHomeCatalogAsync()
        => await this.ExecuteAndAdaptAsync<CategoryDto, Category>(async ()
            => await this.categoryQuery.GetHomeCatalog());

    public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        => await this.ExecuteAndAdaptAsync<CategoryDto, Category>(async ()
            => await this.categoryQuery.GetCategoryAsync(categoryId));

    public async Task<ProductDto> GetProductAsync(int productId)
    => await this.ExecuteAndAdaptAsync<ProductDto, Product>(async ()
        => await this.productQuery.GetProductAsync(productId));


}
