using Ardalis.GuardClauses;
using eShopCloudNative.Architecture.Bootstrap;
using eShopCloudNative.Architecture.Data;
using eShopCloudNative.Architecture.Minio;
using eShopCloudNative.Catalog.Data;
using eShopCloudNative.Catalog.Data.Mappings;
using eShopCloudNative.Catalog.Entities;
using Minio;
using System.Text.RegularExpressions;
using System.Text;
using ISession = NHibernate.ISession;
using Newtonsoft.Json;
using eShopCloudNative.Architecture.Bootstrap.Postgres;
using Serilog;
using System.Diagnostics;

namespace eShopCloudNative.Catalog.Bootstrapper.Sample;
internal class SampleDataBootstrapperService : IBootstrapperService
{
    private ServiceProvider serviceProvider;
    private IServiceScope scope;
    private ISession session;

    public IMinioClientAdapter Minio { get; set; }

    public IConfiguration Configuration { get; set; }

    public string BucketName { get; set; }

    public Task InitializeAsync()
    {
        Guard.Against.Null(this.Minio, nameof(this.Minio));
        Guard.Against.Null(this.Configuration, nameof(this.Configuration));
        Guard.Against.NullOrWhiteSpace(this.BucketName, nameof(this.BucketName));

        if (this.Configuration.GetValue<bool>("boostrap:sample-data"))
        {
            this.serviceProvider = new ServiceCollection()
                .AddNHibernate(cfg => cfg
                .Schema(CatalogConstants.Schema)
                .ConnectionStringKey("catalog")
                .AddMappingsFromAssemblyOf<CategoryMapping>()
                .RegisterSession()
            )
            .AddTransient(sp => this.Configuration)
            .BuildServiceProvider();

        }
        return Task.CompletedTask;
    }

    public async Task ExecuteAsync()
    {
        Log.Information("{svc} Iniciando... ", nameof(SampleDataBootstrapperService));

        if (this.Configuration.GetValue<bool>("boostrap:sample-data"))
        {
            using var scope = this.serviceProvider.CreateScope();
            this.scope = scope;

            using var session = scope.ServiceProvider.GetRequiredService<ISession>();
            this.session = session;

            await this.CreateSampleData();

            Log.Information("{svc} Finalizado com sucesso!!! ", nameof(SampleDataBootstrapperService));
        }
        else
        {
            Log.Information("{svc} Bootstrap ignorado por configuração ", nameof(SampleDataBootstrapperService));
        }
    }

    List<ICatalogEntity> itensToSave = new List<ICatalogEntity>();

    public static string ToUrlSlug(string value)
    {

        //First to lower case
        value = value.ToLowerInvariant();

        //Remove all accents
        var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
        value = Encoding.ASCII.GetString(bytes);

        //Replace spaces
        value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

        //Remove invalid chars
        value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

        //Trim dashes from end
        value = value.Trim('-', '_');

        //Replace double occurences of - or _
        value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

        return value;
    }

    private T AddToSave<T>(T item)
        where T : ICatalogEntity
    {

        this.itensToSave.Add(item);

        return item;
    }

    private Random random = new Random();

    private async Task<Product> CreateFakeProductAsync(string productName, params Category[] categories)
    {
        var product = new Product()
        {
            Name = productName,
            Slug = ToUrlSlug(productName),
            Active = true,
            Description
            = productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine
            + productName + Environment.NewLine,
            Price = random.Next(100,1000),
            Categories = new List<Category>(categories)
        };

        this.AddToSave(product);

        await this.UploadImage(this.AddToSave(new Image() { Product = product, ImageId = Guid.NewGuid(), Index = 0, FileName = "generic1.jpg" }));
        await this.UploadImage(this.AddToSave(new Image() { Product = product, ImageId = Guid.NewGuid(), Index = 1, FileName = "generic2.jpg" }));
        await this.UploadImage(this.AddToSave(new Image() { Product = product, ImageId = Guid.NewGuid(), Index = 2, FileName = "generic3.jpg" }));
        await this.UploadImage(this.AddToSave(new Image() { Product = product, ImageId = Guid.NewGuid(), Index = 3, FileName = "generic4.jpg" }));

        return product;
    }


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0009:Member access should be qualified.", Justification = "<Pending>")]
    private async Task CreateSampleData()
    {
        int automaticItens = 3;

        if (session.Query<CategoryType>().ToList().Count == 0)
        {
            Log.Information("{svc} Criando dados em memória...", nameof(SampleDataBootstrapperService));

            var categoryTypeNormal = AddToSave(new CategoryType() { CategoryTypeId = 1, Name = "Normal", IsHomeShowCase = false, ShowOnMenu = true });

            var categoryVitrine = AddToSave(new CategoryType() { CategoryTypeId = 2, Name = "Vitrine", IsHomeShowCase = true, ShowOnMenu = false });

            var catVitrine1 = AddToSave(new Category() { Name = "Vitrine1", Slug = "v1", CategoryType = categoryVitrine, Active = true, Description = "" });

            var catVitrine2 = AddToSave(new Category() { Name = "Vitrine2", Slug = "v2", CategoryType = categoryVitrine, Active = true, Description = "" });

            var cat_Esporte = AddToSave(new Category() { Name = "Esporte", Slug = "esporte", Icon="<i class=\"fa-sharp fa-solid fa-shirt\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });

            var cat_Esporte_ArLivre = AddToSave(new Category() {Parent = cat_Esporte, Name = "Ar Livre", Slug = "ar-livre", Icon = "<i class=\"fa-solid fa-person-biking\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Esport ao ar livre {i}", cat_Esporte_ArLivre);
            }

            var cat_Esporte_ArLivre_Biking = AddToSave(new Category() {Parent = cat_Esporte_ArLivre, Name = "Biking", Slug = "biking", Icon = "<i class=\"fa-solid fa-person-biking\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Bike {i}", cat_Esporte_ArLivre_Biking);
            }

            var cat_Esporte_Futebol = AddToSave(new Category() { Parent = cat_Esporte,  Name = "Futebol", Slug = "futebol", Icon="<i class=\"fa-sharp fa-solid fa-shirt\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Camisa {i}", cat_Esporte_Futebol);
            }

            var cat_Eletronicos = AddToSave(new Category() { Name = "Eletrônicos", Slug = "eletronicos", Icon="<i class=\"fa-sharp fa-solid fa-shirt\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });

            var catGames = AddToSave(new Category() { Parent = cat_Eletronicos,  Name = "Games", Slug = "jogos", Icon = "<i class=\"fa-solid fa-gamepad-modern\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Game {i}", catGames);
            }

            var cat_Eletronicos_Computadorers = AddToSave(new Category() { Parent = cat_Eletronicos, Name = "Computadores", Slug = "computadores", Icon = "<i class=\"fa-solid fa-desktop\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Computador {i}", cat_Eletronicos_Computadorers);
            }

            var cat_Eletronicos_Brinquedos = AddToSave(new Category() { Parent = cat_Eletronicos, Name = "Brinquedos Eletrônicos", Slug = "brinquedos-eletronicos", Icon = "<i class=\"fa-solid fa-cars\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Brinquedos {i}", cat_Eletronicos_Brinquedos);
            }

            var cat_Eletronicos_SmartWatch = AddToSave(new Category() { Parent = cat_Eletronicos, Name = "SmartWatch", Slug = "smartwatch", Icon = "<i class=\"fa-solid fa-watch-apple\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });
            {
                for (var i = 1; i <= automaticItens; i++)
                    await CreateFakeProductAsync($"Camisa {i}", cat_Eletronicos_SmartWatch);
            }

            var cat_Esporte_Camisas_Selecao = AddToSave(new Category() { Parent = cat_Esporte_Futebol, Name = "Camisas da Seleção", Slug = "selecao", Icon="<i class=\"fa-sharp fa-solid fa-shirt\"></i>", CategoryType = categoryTypeNormal, Active = true, Description = "" });

            var camisaSelecaoAmarela = AddToSave(new Product() { Name = "Camisa Seleção Brasileira - Amarela", Slug = "camisa-selecao-brasileira-amarela", Active = true, Description = "", Price = 5, Categories = new List<Category>() { cat_Esporte, cat_Esporte_Futebol, cat_Esporte_Camisas_Selecao, catVitrine1 } });
            {
                var imagem1 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoAmarela, ImageId = Guid.NewGuid(), Index = 0, FileName = "camisa-brasil-amarela1.webp" }));

                var imagem2 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoAmarela, ImageId = Guid.NewGuid(), Index = 1,FileName = "camisa-brasil-amarela2.webp" }));
            }

            var camisaSelecaoAzul = AddToSave(new Product() { Name = "Camisa Seleção Brasileira - Azul", Slug = "camisa-selecao-brasileira-azul", Active = true, Description = "", Price = 5, Categories = new List<Category>() { cat_Esporte, cat_Esporte_Futebol, cat_Esporte_Camisas_Selecao, catVitrine1 } });
            {
                var imagem1 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoAzul, ImageId = Guid.NewGuid(), Index = 0, FileName = "camisa-brasil-azul1.webp" }));

                var imagem2 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoAzul , ImageId = Guid.NewGuid(),Index = 1, FileName = "camisa-brasil-azul2.webp" }));
            }

            var camisaSelecaoPreta = AddToSave(new Product() { Name = "Camisa Seleção Brasileira - Preta", Slug = "camisa-selecao-brasileira-preta", Active = true, Description = "", Price = 5, Categories = new List<Category>() { cat_Esporte, cat_Esporte_Futebol, cat_Esporte_Camisas_Selecao, catVitrine1 } });
            {
                var imagem1 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoPreta, ImageId = Guid.NewGuid(), Index = 0,FileName = "camisa-brasil-preta1.jpeg" }));

                var imagem2 = await UploadImage(AddToSave(new Image() { Product = camisaSelecaoPreta , ImageId = Guid.NewGuid(),Index = 1, FileName = "camisa-brasil-preta2.jpeg" }));
            }

            var controleXbox = AddToSave(new Product() { Name = "Controle Xbox Elite Wireless Série 2", Slug = "controle-xbox-elite-wireless-serie-2", Active = true, Description = "", Price = 5, Categories = new List<Category>() { catGames, catVitrine2 } });
            {
                var imagem1 = await UploadImage(AddToSave(new Image() { Product = controleXbox, ImageId = Guid.NewGuid(), Index = 0, FileName = "controle-xbox-elite-II-1.jpeg" }));

                var imagem2 = await UploadImage(AddToSave(new Image() { Product = controleXbox , ImageId = Guid.NewGuid(),Index = 1, FileName = "controle-xbox-elite-II-2.jpeg" }));
            }

            Log.Information("{svc} Enviando para o NH...", nameof(SampleDataBootstrapperService));

            foreach (var item in itensToSave)
            {
                await session.SaveAsync(item);
            }

            await session.FlushAsync();

            Log.Information("{svc} FlushAsync() concluído com sucesso!!", nameof(SampleDataBootstrapperService));
        }
        else
        {
            Log.Information("{svc} Ignorado inserts, tabela CategoryType já está prenchida!!", nameof(SampleDataBootstrapperService));
        }
    
    }

    private async Task<Image> UploadImage(Image image)
    {
        Log.Information("{svc} Realizando upload de '{FileName}' para o produto '{ProductName}' da categoria '{CategoryName}'", nameof(SampleDataBootstrapperService), image.FileName, image.Product.Name, image.Product.Categories.First().Name);

        string guid = image.ImageId.ToString();

        await this.Minio.PutObjectAsync((new PutObjectArgs())
                    .WithBucket(this.BucketName)
                    .WithContentType($"image/{Path.GetExtension(image.FileName).Substring(1)}")
                    .WithFileName($"/app/Assets/{image.FileName}")
                    .WithObject(guid)
                    );

        //await this.PreflightAsync(guid);

        return image;
    }

    private async Task PreflightAsync(string guid)
    {
        Log.Information($"{nameof(SampleDataBootstrapperService)} | curl -v --compressed 'http://api:8000/minio/{this.BucketName}/{guid}' -o /tmp/{guid}");
        
        var startInfo = new ProcessStartInfo("curl");
        var arguments = new string[] {
            "-v",
            "--compressed",
            $"'http://api:8000/minio/{this.BucketName}/{guid}'",
            $"-o", $"/tmp/{guid}",
        };
        foreach (string argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        Process curl = Process.Start(startInfo);
        await curl.WaitForExitAsync();
    }
}
