using System.Net.Http.Json;
using PokemonPortfolio.Application.Interfaces;
using PokemonPortfolio.Application.Models.External;

namespace PokemonPortfolio.Infrastructure.Clients;

public sealed class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;

    public PokeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<PokeApiPokemonResponse?> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default)
    {
        return _httpClient.GetFromJsonAsync<PokeApiPokemonResponse>($"pokemon/{nameOrId}", cancellationToken);
    }

    public Task<PokeApiSpeciesResponse?> GetSpeciesAsync(int pokemonId, CancellationToken cancellationToken = default)
    {
        return _httpClient.GetFromJsonAsync<PokeApiSpeciesResponse>($"pokemon-species/{pokemonId}", cancellationToken);
    }

    public Task<PokeApiEvolutionChainResponse?> GetEvolutionChainAsync(int evolutionChainId, CancellationToken cancellationToken = default)
    {
        return _httpClient.GetFromJsonAsync<PokeApiEvolutionChainResponse>($"evolution-chain/{evolutionChainId}", cancellationToken);
    }
}
