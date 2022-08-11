namespace eShopCloudNative.Catalog.Dto;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public IList<Image>? Images { get; set; }
}
