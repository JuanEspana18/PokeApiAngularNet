using Moq;
using PokemonPortfolio.Application.Interfaces;
using PokemonPortfolio.Application.Models.External;
using PokemonPortfolio.Application.Services;
using PokemonPortfolio.Domain.Exceptions;
using Xunit;

namespace PokemonPortfolio.Tests;

public sealed class PokemonServiceTests
{
    private readonly Mock<IPokeApiClient> _pokeApiClientMock = new();
    private readonly Mock<ICacheService> _cacheServiceMock = new();

    [Fact]
    public async Task GetPokemonAsync_ShouldReturnMappedPokemon_WhenExternalDataExists()
    {
        // Arrange
        const string input = "pikachu";

        _cacheServiceMock
            .Setup(cache => cache.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<Application.DTOs.PokemonDetailDto>>>(),
                It.IsAny<TimeSpan>()))
            .Returns<string, Func<Task<Application.DTOs.PokemonDetailDto>>, TimeSpan>((_, factory, _) => factory());

        _pokeApiClientMock
            .Setup(client => client.GetPokemonAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(BuildPokemon());

        _pokeApiClientMock
            .Setup(client => client.GetSpeciesAsync(25, It.IsAny<CancellationToken>()))
            .ReturnsAsync(BuildSpecies());

        _pokeApiClientMock
            .Setup(client => client.GetEvolutionChainAsync(10, It.IsAny<CancellationToken>()))
            .ReturnsAsync(BuildEvolution());

        var service = new PokemonService(_pokeApiClientMock.Object, _cacheServiceMock.Object);

        // Act
        var result = await service.GetPokemonAsync(input);

        // Assert
        Assert.Equal(25, result.Id);
        Assert.Equal("pikachu", result.Name);
        Assert.Equal("yellow", result.Color);
        Assert.Equal(3, result.EvolutionChain.Count);
        Assert.Contains(result.Types, type => type.Name == "electric");
        Assert.Contains(result.Abilities, ability => ability.Name == "static");
    }

    [Fact]
    public async Task GetPokemonAsync_ShouldThrowNotFound_WhenPokemonDoesNotExist()
    {
        _cacheServiceMock
            .Setup(cache => cache.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<Application.DTOs.PokemonDetailDto>>>(),
                It.IsAny<TimeSpan>()))
            .Returns<string, Func<Task<Application.DTOs.PokemonDetailDto>>, TimeSpan>((_, factory, _) => factory());

        _pokeApiClientMock
            .Setup(client => client.GetPokemonAsync("missingno", It.IsAny<CancellationToken>()))
            .ReturnsAsync((PokeApiPokemonResponse?)null);

        var service = new PokemonService(_pokeApiClientMock.Object, _cacheServiceMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => service.GetPokemonAsync("missingno"));
    }

    [Fact]
    public async Task GetBasicPokemonAsync_ShouldReturnBasicShape()
    {
        _cacheServiceMock
            .Setup(cache => cache.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<Application.DTOs.PokemonBasicDto>>>(),
                It.IsAny<TimeSpan>()))
            .Returns<string, Func<Task<Application.DTOs.PokemonBasicDto>>, TimeSpan>((_, factory, _) => factory());

        _pokeApiClientMock
            .Setup(client => client.GetPokemonAsync("pikachu", It.IsAny<CancellationToken>()))
            .ReturnsAsync(BuildPokemon());

        var service = new PokemonService(_pokeApiClientMock.Object, _cacheServiceMock.Object);

        var result = await service.GetBasicPokemonAsync("pikachu");

        Assert.Equal("pikachu", result.Name);
        Assert.True(result.Stats.Count > 0);
    }

    private static PokeApiPokemonResponse BuildPokemon() =>
        new()
        {
            Id = 25,
            Name = "pikachu",
            Height = 4,
            Weight = 60,
            Types =
            [
                new PokemonTypeEntry
                {
                    Type = new NamedApiResource { Name = "electric", Url = "https://pokeapi.co/api/v2/type/13/" }
                }
            ],
            Abilities =
            [
                new PokemonAbilityEntry
                {
                    Ability = new NamedApiResource { Name = "static", Url = "https://pokeapi.co/api/v2/ability/9/" },
                    IsHidden = false
                }
            ],
            Stats =
            [
                new PokemonStatEntry
                {
                    BaseStat = 35,
                    Stat = new NamedApiResource { Name = "hp", Url = "https://pokeapi.co/api/v2/stat/1/" }
                }
            ],
            Sprites = new PokemonSprites
            {
                Other = new OtherSprites
                {
                    OfficialArtwork = new OfficialArtwork
                    {
                        FrontDefault = "https://img/pikachu.png"
                    }
                }
            }
        };

    private static PokeApiSpeciesResponse BuildSpecies() =>
        new()
        {
            BaseHappiness = 50,
            Color = new NamedApiResource { Name = "yellow", Url = "" },
            Habitat = new NamedApiResource { Name = "forest", Url = "" },
            IsLegendary = false,
            IsMythical = false,
            EvolutionChain = new EvolutionChainLink
            {
                Url = "https://pokeapi.co/api/v2/evolution-chain/10/"
            }
        };

    private static PokeApiEvolutionChainResponse BuildEvolution() =>
        new()
        {
            Chain = new EvolutionNode
            {
                Species = new NamedApiResource { Name = "pichu", Url = "" },
                EvolvesTo =
                [
                    new EvolutionNode
                    {
                        Species = new NamedApiResource { Name = "pikachu", Url = "" },
                        EvolvesTo =
                        [
                            new EvolutionNode
                            {
                                Species = new NamedApiResource { Name = "raichu", Url = "" }
                            }
                        ]
                    }
                ]
            }
        };
}
