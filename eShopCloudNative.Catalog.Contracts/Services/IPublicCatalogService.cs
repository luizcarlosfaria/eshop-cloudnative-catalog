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
    [Get("/Public/Catalog/HomeCatalog")]
    Task<IEnumerable<CategoryDto>> GetHomeCatalog();


    [Get("/Public/Catalog/CategoriesForMenu")]
    Task<IEnumerable<CategoryDto>> GetCategoriesForMenu();
}
