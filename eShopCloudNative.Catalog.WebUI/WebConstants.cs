namespace eShopCloudNative.Catalog;

public class WebConstants
{
    private static WebConstants webConstants = new WebConstants();

    public static WebConstants Instance => webConstants;

    public TimeSpan MenuCacheTimeout { get; set; }
    public TimeSpan HomeCatalogCacheTimeout { get; set; }
}
