using Microsoft.Extensions.DependencyInjection;
using PriseApi.Helper;
using PriseApi.Repositories;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;

namespace PriseApi;

public class GlobalServiceRegistrator : IServiceRegistrator
{
    public void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options => options.AddPolicy("ServiceOrigin", builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

        serviceCollection.AddSingleton<IDocumentStore>(serviceProvider =>
            new DocumentStore()
            {
                Urls = new[] { "https://a.free.mlindner.ravendb.cloud" },
                Database = "PriseApp",
                Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2("env/free.mlindner.client.certificate.pfx")
            }.Initialize());

        serviceCollection.AddScoped(serviceProvider => serviceProvider.GetRequiredService<IDocumentStore>().OpenAsyncSession());

        serviceCollection.AddScoped<SpruchRepository>();
        serviceCollection.AddScoped<Random>();

    }
}
