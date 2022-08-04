

using eShopCloudNative.Catalog.Bootstrapper;
using Spring.Context.Support;

XmlApplicationContext context = new XmlApplicationContext("./bootstrapper.xml");


var init = context.GetObject<IBootstrapperService>("BootstrapperService");

init.Initialize();
init.Execute();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

