using System.Text.Json.Serialization;

namespace PokemonPortfolio.Application.Models.External;

public sealed class PokeApiSpeciesResponse
{
    [JsonPropertyName("base_happiness")]
    public int BaseHappiness { get; init; }

    [JsonPropertyName("is_legendary")]
    public bool IsLegendary { get; init; }

    [JsonPropertyName("is_mythical")]
    public bool IsMythical { get; init; }

    public NamedApiResource? Habitat { get; init; }
    public NamedApiResource? Color { get; init; }

    [JsonPropertyName("evolution_chain")]
    public EvolutionChainLink EvolutionChain { get; init; } = new();
}

public sealed class EvolutionChainLink
{
    public string Url { get; init; } = string.Empty;
}
