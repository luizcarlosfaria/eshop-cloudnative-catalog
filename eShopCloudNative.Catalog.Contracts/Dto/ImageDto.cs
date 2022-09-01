namespace eShopCloudNative.Catalog.Dto;

public class ImageDto
{
    public Guid ImageId { get; set; }
    public ProductDto? Product { get; set; }
    public string? FileName { get; set; }
}
