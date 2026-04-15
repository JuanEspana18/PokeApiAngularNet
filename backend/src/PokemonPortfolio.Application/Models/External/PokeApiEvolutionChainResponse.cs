using System.Text.Json.Serialization;

namespace PokemonPortfolio.Application.Models.External;

public sealed class PokeApiEvolutionChainResponse
{
    public EvolutionNode Chain { get; init; } = new();
}

public sealed class EvolutionNode
{
    public NamedApiResource Species { get; init; } = new();

    [JsonPropertyName("evolves_to")]
    public List<EvolutionNode> EvolvesTo { get; init; } = new();
}
