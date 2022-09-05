using eShopCloudNative.Catalog.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;



public interface IPublicCatalogService
{
    [Get("/PublicCatalog/HomeCatalog")]
    Task<IEnumerable<CategoryDto>> GetHomeCatalog();


    [Get("/PublicCatalog/CategoriesForMenu")]
    Task<IEnumerable<CategoryDto>> GetCategoriesForMenu();
}
