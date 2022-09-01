using eShopCloudNative.Catalog.Architecture.Data;
using eShopCloudNative.Catalog.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Bootstrapper.Sample;
internal class SampleDataBootstrapperService : IBootstrapperService
{
    private ServiceProvider serviceProvider;
    private IServiceScope scope;
    private ISession session;
    private MinioClient minio;

    public string BucketName { get; set; }

    public System.Net.NetworkCredential Credentials { get; set; }
    public System.Net.DnsEndPoint ServerEndpoint { get; set; }

    public bool WithSSL { get; set; }

    public Task InitializeAsync(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("boostrap:sample-data"))
        {
            this.serviceProvider = new ServiceCollection()
                .AddNHibernate(cfg => cfg
                .Schema(Constants.Schema)
                .ConnectionStringKey("catalog")
                .AddMappingsFromAssemblyOf<CategoryMapping>()
                .RegisterSession()
            )
            .AddTransient(sp => configuration)
            .BuildServiceProvider();

            this.minio = new MinioClient()
                .WithEndpoint(this.ServerEndpoint.Host, this.ServerEndpoint.Port)
                .WithCredentials(this.Credentials.UserName, this.Credentials.Password);

            this.minio = (this.WithSSL ? minio.WithSSL() : minio).Build();
        }
        return Task.CompletedTask;
    }

    public async Task ExecuteAsync(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("boostrap:sample-data"))
        {
            using var scope = this.serviceProvider.CreateScope();
            this.scope = scope;

            using var session = scope.ServiceProvider.GetRequiredService<ISession>();
            this.session = session;

            await this.ÇreateSampleData();
        }

    }

    private async Task ÇreateSampleData()
    {
        List<ICatalogBaseEntity> itensToSave = new List<ICatalogBaseEntity>();

        if (this.session.Query<CategoryType>().ToList().Count == 0)
        {
            var categoryTypeNormal = new CategoryType() { CategoryTypeId = 1, Name = "Normal", IsHomeShowCase = false, ShowOnMenu = true };
            itensToSave.Add(categoryTypeNormal);

            var categoryVitrine = new CategoryType() { CategoryTypeId = 2, Name = "Vitrine", IsHomeShowCase = true, ShowOnMenu = false };
            itensToSave.Add(categoryVitrine);

            var catCamisas = new Category() { Name = "Camisas", Slug = "camisa", CategoryType = categoryTypeNormal, Active = true, Description = "" };
            itensToSave.Add(catCamisas);

            var catCamisasSelecao = new Category() { Parent = catCamisas, Name = "Camisas da Seleção", Slug = "selecao", CategoryType = categoryTypeNormal, Active = true, Description = "" };
            itensToSave.Add(catCamisasSelecao);

            var catGames = new Category() { Name = "Games", Slug = "jogos", CategoryType = categoryTypeNormal, Active = true, Description = "" };
            itensToSave.Add(catGames);


            var catVitrine1 = new Category() { Name = "Vitrine1", Slug = "v1", CategoryType = categoryVitrine, Active = true, Description = "" };
            itensToSave.Add(catVitrine1);

            var catVitrine2 = new Category() { Name = "Vitrine2", Slug = "v2", CategoryType = categoryVitrine, Active = true, Description = "" };
            itensToSave.Add(catVitrine2);


            var camisaSelecaoAmarela = new Product() { Name = "Camisa Seleção Brasileira - Amarela", Slug = "camisa-selecao-brasileira-amarela", Active = true, Description = "", Price = 5, Categories = new List<Category>() { catCamisas, catCamisasSelecao, catVitrine1 } };
            {
                itensToSave.Add(camisaSelecaoAmarela);

                var imagem1 = new Image() { Product = camisaSelecaoAmarela, ImageId = Guid.NewGuid(), Index = 0, FileName = "camisa-brasil-amarela1.webp" };
                await this.UploadImage(imagem1);
                itensToSave.Add(imagem1);

                var imagem2 = new Image() { Product = camisaSelecaoAmarela, ImageId = Guid.NewGuid(), Index = 1,FileName = "camisa-brasil-amarela2.webp" };
                await this.UploadImage(imagem2);
                itensToSave.Add(imagem2);
            }

            var camisaSelecaoAzul = new Product() { Name = "Camisa Seleção Brasileira - Azul", Slug = "camisa-selecao-brasileira-azul", Active = true, Description = "", Price = 5, Categories = new List<Category>() { catCamisas, catCamisasSelecao, catVitrine1 } };
            {
                itensToSave.Add(camisaSelecaoAzul);

                var imagem1 = new Image() { Product = camisaSelecaoAzul, ImageId = Guid.NewGuid(), Index = 0, FileName = "camisa-brasil-azul1.webp" };
                await this.UploadImage(imagem1);
                itensToSave.Add(imagem1);

                var imagem2 = new Image() { Product = camisaSelecaoAzul , ImageId = Guid.NewGuid(),Index = 1, FileName = "camisa-brasil-azul2.webp" };
                await this.UploadImage(imagem2);
                itensToSave.Add(imagem2);
            }

            var camisaSelecaoPreta = new Product() { Name = "Camisa Seleção Brasileira - Preta", Slug = "camisa-selecao-brasileira-preta", Active = true, Description = "", Price = 5, Categories = new List<Category>() { catCamisas, catCamisasSelecao, catVitrine1 } };
            {
                itensToSave.Add(camisaSelecaoPreta);

                var imagem1 = new Image() { Product = camisaSelecaoPreta, ImageId = Guid.NewGuid(), Index = 0,FileName = "camisa-brasil-preta1.jpg" };
                await this.UploadImage(imagem1);
                itensToSave.Add(imagem1);

                var imagem2 = new Image() { Product = camisaSelecaoPreta , ImageId = Guid.NewGuid(),Index = 1, FileName = "camisa-brasil-preta2.jpg" };
                await this.UploadImage(imagem2);
                itensToSave.Add(imagem2);
            }

            var controleXbox = new Product() { Name = "Controle Xbox Elite Wireless Série 2", Slug = "controle-xbox-elite-wireless-serie-2", Active = true, Description = "", Price = 5, Categories = new List<Category>() { catGames, catVitrine2 } };
            {
                itensToSave.Add(controleXbox);

                var imagem1 = new Image() { Product = controleXbox, ImageId = Guid.NewGuid(), Index = 0, FileName = "controle-xbox-elite-II-1.jpg" };
                await this.UploadImage(imagem1);
                itensToSave.Add(imagem1);

                var imagem2 = new Image() { Product = controleXbox , ImageId = Guid.NewGuid(),Index = 1, FileName = "controle-xbox-elite-II-2.jpg" };
                await this.UploadImage(imagem2);
                itensToSave.Add(imagem2);
            }

            foreach (var item in itensToSave)
                this.session.Save(item);

            this.session.Flush();
        }
    }

    private async Task UploadImage(Image image)
    {
        await this.minio.PutObjectAsync((new PutObjectArgs())
                    .WithBucket(this.BucketName)
                    .WithFileName($"/app/Assets/{image.FileName}")
                    .WithObject(image.ImageId.ToString())
                    );
    }
}
