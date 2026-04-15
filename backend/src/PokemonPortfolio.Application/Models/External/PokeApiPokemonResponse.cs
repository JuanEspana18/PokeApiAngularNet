using System.Text.Json.Serialization;

namespace PokemonPortfolio.Application.Models.External;

public sealed class PokeApiPokemonResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Height { get; init; }
    public int Weight { get; init; }
    public List<PokemonTypeEntry> Types { get; init; } = new();
    public List<PokemonAbilityEntry> Abilities { get; init; } = new();
    public List<PokemonStatEntry> Stats { get; init; } = new();
    public PokemonSprites Sprites { get; init; } = new();
}

public sealed class PokemonTypeEntry
{
    public NamedApiResource Type { get; init; } = new();
}

public sealed class PokemonAbilityEntry
{
    public NamedApiResource Ability { get; init; } = new();

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; init; }
}

public sealed class PokemonStatEntry
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; init; }

    public NamedApiResource Stat { get; init; } = new();
}

public sealed class PokemonSprites
{
    public OtherSprites Other { get; init; } = new();
}

public sealed class OtherSprites
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtwork OfficialArtwork { get; init; } = new();
}

public sealed class OfficialArtwork
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; init; }
}
