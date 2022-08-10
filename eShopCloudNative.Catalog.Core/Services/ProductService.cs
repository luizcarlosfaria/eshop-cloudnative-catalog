using eShopCloudNative.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService: IProductService
{
    public IEnumerable<ProductDto> GetProducts()
    {
        return Enumerable.Range(1, 5).Select(index => new ProductDto
        {
            ProductId = index,
            Name = $"Produto {index}",
            Images = new List<ImageDto> { new ImageDto() { Name = "", ImageId = 0, Bucket = "" } }
        })
        .ToArray();
    }

}
