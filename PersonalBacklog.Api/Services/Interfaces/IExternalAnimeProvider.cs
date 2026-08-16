using PersonalBacklog.Api.DTOs;

namespace PersonalBacklog.Api.Services.Interfaces;

public interface IExternalAnimeProvider
{
    Task<ExternalAnimeResponse?> GetAnimeByIdAsync(int externalId);
}