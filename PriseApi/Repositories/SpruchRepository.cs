using PriseApi.Helper;
using PriseApi.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Diagnostics;

namespace PriseApi.Repositories;
public class SpruchRepository
{
    private readonly IAsyncDocumentSession _session;
    private readonly Random _random;
    private readonly ILogger<SpruchRepository> _logger;

    public SpruchRepository(IAsyncDocumentSession session, Random random, ILogger<SpruchRepository> logger)
    {
        _session = session;
        _random = random;
        _logger = logger;
    }

    public async Task<int> GetCount()
    {
        var config = (await _session.Query<Configuration>().ToListAsync()).First();
        return config.lastPoemIndex;
    }

    public async Task<Poem?> GetAny()
    {
        var config = (await _session.Query<Configuration>().ToListAsync()).First();
        var random = _random.Next(1, config.lastPoemIndex + 1);
        var querry = await _session.Query<Poem>()
                .Where(v => v.Index == random)
                .ToListAsync();
        return querry.FirstOrDefault();
    }

    public async Task<Poem?> Get(int id)
    {
        var querry = await _session.Query<Poem>()
                        .Where(v => v.Index == id)
                        .ToListAsync();
        return querry.FirstOrDefault();
    }
}
