namespace eShopCloudNative.Catalog.Dto;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }

    //public CategoryDto? Parent { get; set; }

    public CategoryTypeDto? CategoryType { get; set; }

    public IList<CategoryDto>? Children { get; set; }

    public IList<ProductDto>? Products { get; set; }

    public string? Icon { get; set; }

    public string? Description { get; set; }

    public string? Slug { get; set; }

    public bool Active { get; set; }


}
