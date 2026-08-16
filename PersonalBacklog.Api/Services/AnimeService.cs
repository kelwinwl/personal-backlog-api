using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Models;
using PersonalBacklog.Api.DTOs;
using PersonalBacklog.Api.Data;
using PersonalBacklog.Api.Services.Interfaces;

namespace PersonalBacklog.Api.Services;

public class AnimeService : IAnimeService
{
    private readonly BacklogDbContext _context;
    private readonly IExternalAnimeProvider _externalProvider;

    public AnimeService(BacklogDbContext context, IExternalAnimeProvider externalProvider)
    {
        _context = context;
        _externalProvider = externalProvider;
    }

    public async Task<IEnumerable<AnimeResponseDto>> GetAllAsync()
    {
        return await _context.Animes.AsNoTracking()
            .Select(a => MapResponseDto(a))
            .ToListAsync();
    }

    public async Task<AnimeResponseDto?> GetByIdAsync(int id)
    {
        var anime = await _context.Animes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return anime != null ? MapResponseDto(anime) : null;
    }

    public async Task<IEnumerable<AnimeResponseDto>> SearchTitleAsync(string query)
    {
        return await _context.Animes.AsNoTracking()
            .Where(a => EF.Functions.ILike(a.Title, $"%{query}%"))
            .Select(a => MapResponseDto(a))
            .ToListAsync();
    }

    public async Task<AnimeResponseDto> CreateAnimeAsync(CreateAnimeDto dto)
    {
        var anime = new Anime()
        {
            MalId = dto.MalId,
            Title = dto.Title,
            Description = dto.Description,
            TotalEpisodes = dto.TotalEpisodes,
            ImageUrl = dto.ImageUrl
        };
        
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
        
        return MapResponseDto(anime);
    }

    public async Task<bool> UpdateAnimeAsync(int id, UpdateAnimeDto dto)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null) return false;
        
        anime.Title = dto.Title;
        anime.Description = dto.Description;
        anime.TotalEpisodes = dto.TotalEpisodes;
        anime.ImageUrl = dto.ImageUrl;
        
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<AnimeResponseDto?> ImportFromExternalAsync(int malId)
    {
        var existingAnime = await _context.Animes.AsNoTracking()
            .FirstOrDefaultAsync(a => a.MalId == malId);
        if (existingAnime != null) return MapResponseDto(existingAnime);
        
        var externalData = await _externalProvider.GetAnimeByIdAsync(malId);
        if (externalData == null) return null;

        var anime = new Anime
        {
            MalId = externalData.ExternalId,
            Title = externalData.Title,
            Description = externalData.Description,
            TotalEpisodes = externalData.TotalEpisodes,
            ImageUrl = externalData.ImageUrl
        };
        
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
        
        return MapResponseDto(anime);
    }

    private static AnimeResponseDto MapResponseDto(Anime anime) => new(
        anime.Id,
        anime.MalId,
        anime.Title,
        anime.Description,
        anime.TotalEpisodes,
        anime.ImageUrl,
        anime.DateUpdated
        );
}