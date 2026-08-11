using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Data;
using PersonalBacklog.Api.Models;

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
        {
            return NotFound();
        }
        
        return Ok(anime);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> AnimeUpdate(int id, [FromBody] Anime anime)
    {
        if (id != anime.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> AnimeDelete(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null)
        {
            return NotFound();
        }
        
        _context.Animes.Remove(anime);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}