using PersonalBacklog.Api.DTOs;

namespace PersonalBacklog.Api.Services.Interfaces;

public interface IAnimeService
{
    Task<IEnumerable<AnimeResponseDto>> GetAllAsync();
    Task<AnimeResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<AnimeResponseDto>> SearchTitleAsync(string query);
    Task<AnimeResponseDto> CreateAnimeAsync(CreateAnimeDto dto);
    Task<bool> UpdateAnimeAsync(int id, UpdateAnimeDto dto);
    Task<AnimeResponseDto?> ImportFromExternalAsync(int malId);
}