using eShopCloudNative.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService : IProductService
{
    public Task<IEnumerable<Product>> GetHomeCatalog()
    {
        var returnValue = Enumerable.Range(1, 8).Select(index => new Product
        {
            ProductId = index,
            Name = $"Produto {index}",
            Description = $"Produto {index}",
            Slug = $"{index:00000}--Produto-{index}",
            Images = new List<Image> {
                new Image() {
                    ImageId = 0,
                    Name = "",
                    Bucket = ""
                }
            }
        });
        return Task.FromResult(returnValue);
    }

}
