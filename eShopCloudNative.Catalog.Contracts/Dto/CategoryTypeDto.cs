namespace eShopCloudNative.Catalog.Dto;

public class CategoryTypeDto
{
    public int CategoryTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public IList<CategoryDto>? Categories { get; set; }

    public bool? ShowOnMenu { get; set; }

    public bool? IsHomeShowCase { get; set; }
}