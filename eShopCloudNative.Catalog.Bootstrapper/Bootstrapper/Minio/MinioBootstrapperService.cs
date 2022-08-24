using Microsoft.Extensions.Configuration;
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

    public async Task InitializeAsync(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("boostrap:minio"))
        {
            this.minio = new MinioClient()
                .WithEndpoint(this.ServerEndpoint.Host, this.ServerEndpoint.Port)
                .WithCredentials(this.Credentials.UserName, this.Credentials.Password);

            if (this.WithSSL)
            {
                this.minio = this.minio.WithSSL();
            }

            this.minio = this.minio.Build();

            this.oldBuckets = (await this.minio.ListBucketsAsync()).Buckets;
        }
        else
        {
            //TODO: Logar dizendo que está ignorando
        }
    }

    public async Task ExecuteAsync(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("boostrap:minio"))
        {
            foreach (var bucketName in this.BucketsToCreate)
            {
                if (oldBuckets.Any(it => it.Name == bucketName) == false)
                {
                    await this.minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }
            }
        }
        else
        {
            //TODO: Logar dizendo que está ignorando
        }
    }

}
