using PokemonPortfolio.Application.DTOs;

namespace PokemonPortfolio.Application.Interfaces;

public interface IPokemonService
{
    Task<PokemonDetailDto> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default);
    Task<PokemonBasicDto> GetBasicPokemonAsync(string nameOrId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PokemonListItemDto>> SearchPokemonAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PokemonEvolutionDto>> GetEvolutionAsync(string nameOrId, CancellationToken cancellationToken = default);
}
