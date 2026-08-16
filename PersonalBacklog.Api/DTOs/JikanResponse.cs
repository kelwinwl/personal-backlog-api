using System.Text.Json.Serialization;

namespace PersonalBacklog.Api.DTOs;

public record JikanResponse(
    [property: JsonPropertyName("data")] JikanAnimeData Data
);

public record JikanAnimeData(
    [property: JsonPropertyName("mal_id")] int MalId,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("synopsis")] string? Synopsis,
    [property: JsonPropertyName("episodes")] int? Episodes,
    [property: JsonPropertyName("status")] string? Status,
    [property: JsonPropertyName("images")] JikanImages? Images
);

public record JikanImages(
    [property: JsonPropertyName("jpg")] JikanImageFormats? Jpg
);

public record JikanImageFormats(
    [property: JsonPropertyName("image_url")] string? ImageUrl,
    [property: JsonPropertyName("large_image_url")] string? LargeImageUrl
    );