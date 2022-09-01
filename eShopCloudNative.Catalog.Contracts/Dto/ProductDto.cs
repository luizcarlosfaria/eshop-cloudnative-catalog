namespace eShopCloudNative.Catalog.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public IList<CategoryDto>? Categories { get; set; }
    public IList<ImageDto>? Images { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
}
