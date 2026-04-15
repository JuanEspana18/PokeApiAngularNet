using Microsoft.Extensions.Caching.Memory;
using PokemonPortfolio.Application.Interfaces;

namespace PokemonPortfolio.Infrastructure.Caching;

public sealed class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
    {
        if (_memoryCache.TryGetValue(key, out T? cached) && cached is not null)
        {
            return cached;
        }

        var result = await factory();
        _memoryCache.Set(key, result, expiration);

        return result;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}
