using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Data;
using PersonalBacklog.Api.Models;
using PersonalBacklog.Api.Services;

namespace PersonalBacklog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly BacklogDbContext _context;

    public AnimeController(BacklogDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AnimeCreate([FromBody]Anime anime)
    {
        _context.Animes.Add(anime);

        await _context.SaveChangesAsync();
        
        return Created($"/api/anime/{anime.Id}", anime);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnimes()
    {
        var animes = await _context.Animes.ToListAsync();
        
        return Ok(animes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimeByID(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null)
            return NotFound();
        
        
        return Ok(anime);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAnimeByQuery([FromQuery] string name)
    {
        var animeFiltered = await _context.Animes
            .Where(a => a.Title.Contains(name))
            .ToListAsync();
                         
        return Ok(animeFiltered);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> AnimeUpdate(int id, [FromBody] Anime anime)
    {
        if (id != anime.Id)
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        
        
        _context.Entry(anime).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Animes.Any(a => a.Id == id))
                return NotFound();
        }

        return NoContent();
    }

    [HttpPost("import/{malId}")]
    public async Task<IActionResult> ImportAnimeFromJikan(int malId, [FromServices] JikanApiServices jikanServices)
    {
        var existingName = await _context.Animes.FirstOrDefaultAsync(a => a.MalId == malId);
        if (existingName != null)
            return BadRequest($"Anime with MyAnimeList ID {malId} is already in your backlog");
        
        var jikanData = await jikanServices.GetAnimeByIdAsync(malId);
        
        var newAnime = new Anime
        {
            MalId = jikanData.MalId,
            Title = jikanData.Title,
            Description = jikanData.Synopsis,
            TotalEpisodes = jikanData.Episodes ?? 0,
            ImageUrl = jikanData.Images?.Jpg?.LargeImageUrl ?? jikanData.Images?.Jpg?.ImageUrl,
            DateUpdated = DateTime.UtcNow
        };

        _context.Animes.Add(newAnime);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetAnimeByID), new { id = newAnime.Id }, newAnime);
    }
}