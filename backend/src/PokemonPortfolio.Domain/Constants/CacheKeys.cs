namespace PokemonPortfolio.Domain.Constants;

public static class CacheKeys
{
    public static string PokemonDetail(string nameOrId) =>
        $"pokemon:detail:{nameOrId.Trim().ToLowerInvariant()}";

    public static string PokemonBasic(string nameOrId) =>
        $"pokemon:basic:{nameOrId.Trim().ToLowerInvariant()}";
}
