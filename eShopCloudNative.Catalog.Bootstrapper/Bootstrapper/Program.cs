using eShopCloudNative.Catalog.Bootstrapper;
using Spring.Context.Support;

XmlApplicationContext context = new XmlApplicationContext("./bootstrapper.xml");


var init = context.GetObject<IBootstrapperService>("BootstrapperService");

await init.InitializeAsync();
await init.ExecuteAsync();

