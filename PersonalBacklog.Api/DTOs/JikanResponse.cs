using System.Text.Json.Serialization;

namespace PersonalBacklog.Api.DTOs;

public class JikanResponse
{
    [JsonPropertyName("data")] 
    public required JikanAnimeData Data { get; set; }
}

public class JikanAnimeData
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }

    [JsonPropertyName("title")] 
    public required string Title { get; set; }
    
    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set; }
    
    [JsonPropertyName("episodes")]
    public int? Episodes { get; set; }
    
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    
    [JsonPropertyName("images")]
    public JikanImages? Images { get; set; }
}

public class JikanImages
{
    [JsonPropertyName("jpg")]
    public JikanImageFormats? Jpg { get; set; }
}

public class JikanImageFormats
{
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
    
    [JsonPropertyName("large_image_url")]
    public string? LargeImageUrl { get; set; }
}