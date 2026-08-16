using Microsoft.AspNetCore.Mvc;
using PersonalBacklog.Api.Services.Interfaces;
using PersonalBacklog.Api.DTOs;

namespace PersonalBacklog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeController(IAnimeService animeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AnimeCreate([FromBody]CreateAnimeDto dto)
    {
     var anime = await animeService.CreateAnimeAsync(dto);
     
        return CreatedAtAction(nameof(GetAnimeById), new { id = anime.Id }, anime);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnimes()
    {
        var animes = await animeService.GetAllAsync();
        
        return Ok(animes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimeById(int id)
    {
        var anime = await animeService.GetByIdAsync(id);
        if (anime == null)
            return NotFound();
        
        return Ok(anime);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAnimeByQuery([FromQuery] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Search query cannot be empty. Please provide a name parameter.");
        
        var animeSearch = await animeService.SearchTitleAsync(name);
                         
        return Ok(animeSearch);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> AnimeUpdate(int id, [FromBody] UpdateAnimeDto anime)
    {
        var success = await animeService.UpdateAnimeAsync(id, anime);

        if (!success)
            return NotFound($"Anime with ID {id} does not exist");
        
        return NoContent();
    }

    [HttpPost("import/{malId}")]
    public async Task<IActionResult> ImportAnimeFromJikan(int malId)
    {
        var animeImport = await animeService.ImportFromExternalAsync(malId);

        if (animeImport == null)
            return NotFound($"Anime with MyAnimeList ID {malId} was not found.");
        
        return Ok(animeImport);
    }
}