using System.Text.Json;
using PersonalBacklog.Api.DTOs;
using PersonalBacklog.Api.Services.Interfaces;

namespace PersonalBacklog.Api.Services;

public class JikanApiService : IExternalAnimeProvider
{
    private readonly HttpClient _httpClient;
    
    public JikanApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    }

    public async Task<ExternalAnimeResponse?> GetAnimeByIdAsync(int externalId)
    {
        var response = await _httpClient.GetAsync($"anime/{externalId}");

        if (!response.IsSuccessStatusCode) return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        
        var jikanData = JsonSerializer.Deserialize<JikanResponse>(jsonString);
        
        if (jikanData?.Data == null) return null;
        
        return new ExternalAnimeResponse(
            ExternalId: jikanData.Data.MalId,
            Title: jikanData.Data.Title,
            Description: jikanData.Data.Synopsis,
            TotalEpisodes: jikanData.Data.Episodes ?? 0,
            ImageUrl: jikanData.Data.Images?.Jpg?.LargeImageUrl ?? jikanData.Data.Images?.Jpg?.ImageUrl
            );
    }
}