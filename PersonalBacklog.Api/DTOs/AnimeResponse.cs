namespace PersonalBacklog.Api.DTOs;

public record AnimeResponseDto(
    int Id,
    int? MalId,
    string Title,
    string? Description,
    int TotalEpisodes,
    string? ImageUrl,
    DateTime DateUpdated
);

public record CreateAnimeDto(
    int? MalId,
    string Title,
    string? Description,
    int TotalEpisodes,
    string? ImageUrl
);

public record UpdateAnimeDto(
    string Title,
    string? Description,
    int TotalEpisodes,
    string? ImageUrl
);