using eShopCloudNative.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService : IProductService
{
    public Task<IEnumerable<ProductDto>> GetHomeCatalog()
    {
        var returnValue = Enumerable.Range(1, 8).Select(index => new ProductDto
        {
            ProductId = index,
            Name = $"Produto {index}",
            Description = $"Produto {index}",
            Slug = $"{index:00000}--Produto-{index}",
            Images = new List<ImageDto> {
                new ImageDto() {
                    ImageId = 0,
                    Name = "",
                    Bucket = ""
                }
            }
        });
        return Task.FromResult(returnValue);
    }

}
