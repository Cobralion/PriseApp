using Raven.Client.Documents;
using Scrapper;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//var list = new Scrapper.Scrapper().Scrap();

using var store = new DocumentStore()
{
    Urls = new[] { "https://a.free.mlindner.ravendb.cloud" },
    Database = "PriseApp",
    Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2("env/free.mlindner.client.certificate.pfx")
};

store.Initialize();

using var session = store.OpenAsyncSession();

//foreach (var item in list.Select((v, i) => new { Index = i, Value = v }))
//{
//    await session.StoreAsync(new Poem(item.Index + 1, item.Value.Header, item.Value.Value));

//    System.Console.WriteLine($"Header:{item.Value.Header}\nValue:{item.Value.Value}");
//    System.Console.WriteLine("----------------");

//    System.Console.WriteLine($"Index: {item.Index}");
//}

await session.StoreAsync(new Configuration(10035));

await session.SaveChangesAsync();


record Configuration(int lastPoemIndex);

record Poem(int Index, string Heading, string Value);