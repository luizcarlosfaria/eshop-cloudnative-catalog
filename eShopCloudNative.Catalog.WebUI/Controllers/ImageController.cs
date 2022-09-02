using Microsoft.AspNetCore.Mvc;
using Minio;

namespace eShopCloudNative.Catalog.Controllers;
public class ImageController : Controller
{

    public ImageController() { }

    [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> IndexAsync(Guid id, [FromServices] MinioClient minioClient)
    {
        if (id != Guid.Empty)
        {
            byte[]? content = default;

            var result = await minioClient.GetObjectAsync(
                new GetObjectArgs()
                .WithObject(id.ToString())
                .WithBucket("catalog-images")
                .WithCallbackStream((stream) =>
                {
                    MemoryStream streamToSend = new MemoryStream();
                    stream.CopyTo(streamToSend);
                    content = streamToSend.ToArray();
                })
            );

            if (content != null)
                return this.File(content, result.ContentType);

        }

        return this.NotFound();
    }

}
