using PokemonPortfolio.Application.DTOs;
using PokemonPortfolio.Application.Interfaces;
using PokemonPortfolio.Application.Models.External;
using PokemonPortfolio.Domain.Constants;
using PokemonPortfolio.Domain.Exceptions;

namespace PokemonPortfolio.Application.Services;

public sealed class PokemonService : IPokemonService
{
    private readonly IPokeApiClient _pokeApiClient;
    private readonly ICacheService _cacheService;

    public PokemonService(IPokeApiClient pokeApiClient, ICacheService cacheService)
    {
        _pokeApiClient = pokeApiClient;
        _cacheService = cacheService;
    }

    public async Task<PokemonDetailDto> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default)
    {
        var normalized = NormalizeNameOrId(nameOrId);
        var cacheKey = CacheKeys.PokemonDetail(normalized);

        var result = await _cacheService.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var pokemon = await _pokeApiClient.GetPokemonAsync(normalized, cancellationToken);
                if (pokemon is null)
                {
                    throw new NotFoundException($"Pokemon '{normalized}' not found.");
                }

                var species = await _pokeApiClient.GetSpeciesAsync(pokemon.Id, cancellationToken);
                if (species is null)
                {
                    throw new ExternalServiceException("Could not retrieve species information.");
                }

                var evolutionChainId = ExtractEvolutionChainId(species.EvolutionChain.Url);
                var evolution = await _pokeApiClient.GetEvolutionChainAsync(evolutionChainId, cancellationToken);

                return MapToDetailDto(pokemon, species, evolution);
            },
            TimeSpan.FromMinutes(30));

        return result!;
    }

    public async Task<PokemonBasicDto> GetBasicPokemonAsync(string nameOrId, CancellationToken cancellationToken = default)
    {
        var normalized = NormalizeNameOrId(nameOrId);
        var cacheKey = CacheKeys.PokemonBasic(normalized);

        var result = await _cacheService.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var pokemon = await _pokeApiClient.GetPokemonAsync(normalized, cancellationToken);
                if (pokemon is null)
                {
                    throw new NotFoundException($"Pokemon '{normalized}' not found.");
                }

                return new PokemonBasicDto
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    ImageUrl = pokemon.Sprites.Other.OfficialArtwork.FrontDefault ?? string.Empty,
                    Height = pokemon.Height,
                    Weight = pokemon.Weight,
                    Types = pokemon.Types.Select(type => new PokemonTypeDto
                    {
                        Name = type.Type.Name
                    }).ToArray(),
                    Abilities = pokemon.Abilities.Select(ability => new PokemonAbilityDto
                    {
                        Name = ability.Ability.Name,
                        IsHidden = ability.IsHidden
                    }).ToArray(),
                    Stats = pokemon.Stats.Select(stat => new PokemonStatDto
                    {
                        Name = stat.Stat.Name,
                        Value = stat.BaseStat
                    }).ToArray()
                };
            },
            TimeSpan.FromMinutes(15));

        return result!;
    }

    public async Task<IReadOnlyCollection<PokemonListItemDto>> SearchPokemonAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Array.Empty<PokemonListItemDto>();
        }

        var pokemon = await _pokeApiClient.GetPokemonAsync(NormalizeNameOrId(name), cancellationToken);

        if (pokemon is null)
        {
            return Array.Empty<PokemonListItemDto>();
        }

        return new[]
        {
            new PokemonListItemDto
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                ImageUrl = pokemon.Sprites.Other.OfficialArtwork.FrontDefault ?? string.Empty
            }
        };
    }

    public async Task<IReadOnlyCollection<PokemonEvolutionDto>> GetEvolutionAsync(string nameOrId, CancellationToken cancellationToken = default)
    {
        var detail = await GetPokemonAsync(nameOrId, cancellationToken);
        return detail.EvolutionChain;
    }

    private static string NormalizeNameOrId(string nameOrId) =>
        nameOrId.Trim().ToLowerInvariant();

    private static int ExtractEvolutionChainId(string url)
    {
        var segments = url.TrimEnd('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        return int.Parse(segments[^1]);
    }

    private static PokemonDetailDto MapToDetailDto(
        PokeApiPokemonResponse pokemon,
        PokeApiSpeciesResponse species,
        PokeApiEvolutionChainResponse? evolution)
    {
        return new PokemonDetailDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            ImageUrl = pokemon.Sprites.Other.OfficialArtwork.FrontDefault ?? string.Empty,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            BaseHappiness = species.BaseHappiness,
            Color = species.Color?.Name ?? string.Empty,
            Habitat = species.Habitat?.Name ?? "unknown",
            IsLegendary = species.IsLegendary,
            IsMythical = species.IsMythical,
            Types = pokemon.Types.Select(type => new PokemonTypeDto
            {
                Name = type.Type.Name
            }).ToArray(),
            Abilities = pokemon.Abilities.Select(ability => new PokemonAbilityDto
            {
                Name = ability.Ability.Name,
                IsHidden = ability.IsHidden
            }).ToArray(),
            Stats = pokemon.Stats.Select(stat => new PokemonStatDto
            {
                Name = stat.Stat.Name,
                Value = stat.BaseStat
            }).ToArray(),
            EvolutionChain = FlattenEvolution(evolution).Select(name => new PokemonEvolutionDto
            {
                Name = name
            }).ToArray()
        };
    }

    private static IReadOnlyCollection<string> FlattenEvolution(PokeApiEvolutionChainResponse? evolution)
    {
        var result = new List<string>();

        if (evolution?.Chain is null)
        {
            return result;
        }

        Traverse(evolution.Chain, result);

        return result.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
    }

    private static void Traverse(EvolutionNode node, ICollection<string> names)
    {
        names.Add(node.Species.Name);

        foreach (var next in node.EvolvesTo)
        {
            Traverse(next, names);
        }
    }
}
