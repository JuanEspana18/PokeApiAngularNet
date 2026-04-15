using PokemonPortfolio.Application.Models.External;

namespace PokemonPortfolio.Application.Interfaces;

public interface IPokeApiClient
{
    Task<PokeApiPokemonResponse?> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default);
    Task<PokeApiSpeciesResponse?> GetSpeciesAsync(int pokemonId, CancellationToken cancellationToken = default);
    Task<PokeApiEvolutionChainResponse?> GetEvolutionChainAsync(int evolutionChainId, CancellationToken cancellationToken = default);
}
