using System.ComponentModel.DataAnnotations;

namespace PersonalBacklog.Api.Models;

public class Anime
{
    public int Id { get; set; }
    public int? MalId { get; set; }

    [Required(ErrorMessage = "The title is required.")]
    [MaxLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int TotalEpisodes { get; set; }
    
    [MaxLength(400)]
    public string? ImageUrl { get; set; }
    
    public DateTime DateUpdated { get; set; }
    
    public ICollection<UserAnime> UserAnimes { get; set; } = new List<UserAnime>();
}