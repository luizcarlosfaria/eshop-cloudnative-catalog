using Minio;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper.Minio;
public class MinioBootstrapperService : IBootstrapperService
{
    public System.Net.NetworkCredential Credentials { get; set; }
    public System.Net.DnsEndPoint ServerEndpoint { get; set; }

    public bool WithSSL { get; set; }

    public string[] BucketsToCreate { get; set; }

    protected MinioClient minio;

    protected List<Bucket> oldBuckets;

    public void Execute()
    {
        foreach (var bucketName in this.BucketsToCreate)
        {
            if (oldBuckets.Any(it => it.Name == bucketName) == false)
            {
                this.minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName)).GetAwaiter().GetResult();
            }
        }
    }

    public void Initialize()
    {
        this.minio = new MinioClient()
            .WithEndpoint(this.ServerEndpoint.Host, this.ServerEndpoint.Port)
            .WithCredentials(this.Credentials.UserName, this.Credentials.Password);

        if (this.WithSSL)
        {
            this.minio = this.minio.WithSSL();
        }

        this.minio = this.minio.Build();

        this.oldBuckets = this.minio.ListBucketsAsync().GetAwaiter().GetResult().Buckets;
    }
}
