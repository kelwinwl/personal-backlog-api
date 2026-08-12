using System.Text.Json;
using PersonalBacklog.Api.DTOs;

namespace PersonalBacklog.Api.Services;

public class JikanApiServices
{
    private readonly HttpClient _httpClient;
    
    public JikanApiServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    }

    public async Task<JikanAnimeData?> GetAnimeByIdAsync(int malId)
    {
        var response = await _httpClient.GetAsync($"anime/{malId}");

        if (!response.IsSuccessStatusCode) return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JikanResponse>(jsonString);

        return result?.Data;
    }
}