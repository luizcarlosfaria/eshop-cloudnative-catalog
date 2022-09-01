using Microsoft.AspNetCore.Mvc;
using Minio;

namespace eShopCloudNative.Catalog.Controllers;
public class ImageController : Controller
{

    public ImageController() { }

    public async Task<IActionResult> IndexAsync(Guid id, [FromServices] MinioClient minioClient)
    {
        if (id != Guid.Empty)
        {
            MemoryStream stremToSend = new MemoryStream();

            var result = await minioClient.GetObjectAsync(
                new GetObjectArgs()
                .WithObject(id.ToString())
                .WithBucket("catalog-images")                
                .WithCallbackStream((stream) => stream.CopyTo(stremToSend) )
            );

            stremToSend.Position = 0;

            return File(stremToSend, "image/jpeg");

        }

        return NotFound();
    }
}
