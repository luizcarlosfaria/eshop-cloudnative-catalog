namespace eShopCloudNative.Catalog.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public IList<ImageDto>? Images { get; set; }
}
