namespace PokemonPortfolio.Application.DTOs;

public sealed class PokemonDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public int Height { get; init; }
    public int Weight { get; init; }
    public int BaseHappiness { get; init; }
    public string Color { get; init; } = string.Empty;
    public string Habitat { get; init; } = string.Empty;
    public bool IsLegendary { get; init; }
    public bool IsMythical { get; init; }
    public IReadOnlyCollection<PokemonTypeDto> Types { get; init; } = Array.Empty<PokemonTypeDto>();
    public IReadOnlyCollection<PokemonAbilityDto> Abilities { get; init; } = Array.Empty<PokemonAbilityDto>();
    public IReadOnlyCollection<PokemonStatDto> Stats { get; init; } = Array.Empty<PokemonStatDto>();
    public IReadOnlyCollection<PokemonEvolutionDto> EvolutionChain { get; init; } = Array.Empty<PokemonEvolutionDto>();
}
