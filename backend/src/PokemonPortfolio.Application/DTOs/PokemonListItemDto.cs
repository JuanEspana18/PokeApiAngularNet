namespace PokemonPortfolio.Application.DTOs;

public sealed class PokemonListItemDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
}
