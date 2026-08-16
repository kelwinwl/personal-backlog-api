namespace PersonalBacklog.Api.DTOs;

public record ExternalAnimeResponse(
    int ExternalId,
    string Title,
    string? Description,
    int TotalEpisodes,
    string? ImageUrl
    );