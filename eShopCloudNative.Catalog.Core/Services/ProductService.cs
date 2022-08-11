using eShopCloudNative.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService : IProductService
{
    public Task<IEnumerable<Product>> GetProducts()
    {
        var returnValue = Enumerable.Range(1, 5).Select(index => new Product
        {
            ProductId = index,
            Name = $"Produto {index}",
            Images = new List<Image> {
                new Image() {
                    Name = "",
                    ImageId = 0,
                    Bucket = ""
                }
            }
        });
        return Task.FromResult(returnValue);
    }

}
