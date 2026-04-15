using Microsoft.AspNetCore.Mvc;
using PokemonPortfolio.Application.Interfaces;

namespace PokemonPortfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("{nameOrId}")]
    public async Task<IActionResult> Get(string nameOrId, CancellationToken cancellationToken)
    {
        var result = await _pokemonService.GetPokemonAsync(nameOrId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{nameOrId}/basic")]
    public async Task<IActionResult> GetBasic(string nameOrId, CancellationToken cancellationToken)
    {
        var result = await _pokemonService.GetBasicPokemonAsync(nameOrId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{nameOrId}/evolution")]
    public async Task<IActionResult> GetEvolution(string nameOrId, CancellationToken cancellationToken)
    {
        var result = await _pokemonService.GetEvolutionAsync(nameOrId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name, CancellationToken cancellationToken)
    {
        var result = await _pokemonService.SearchPokemonAsync(name, cancellationToken);
        return Ok(result);
    }
}
