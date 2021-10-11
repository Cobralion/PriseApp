using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using PriseApi;
using PriseApi.Helper;
using PriseApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices<GlobalServiceRegistrator>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("ServiceOrigin");

app.MapGet("/sprueche/{id}", async (SpruchRepository repo, int id) =>
{
    var v = await repo.Get(id);
    return v is null ? Results.NotFound() : Results.Ok(v);
});

app.MapGet("/sprueche/count", async (SpruchRepository repo) => await repo.GetCount());

app.MapGet("/sprueche/any", async (SpruchRepository repo) => await repo.GetAny());

app.Run();
