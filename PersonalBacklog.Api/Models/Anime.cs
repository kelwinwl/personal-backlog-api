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
    
    public int CurrentEpisodes { get; set; }

    [Required]
    [MaxLength(30)]
    [AllowedValues("Plan to Watch", "Watching", "Completed", "Dropped", "Paused", ErrorMessage = "Invalid status provided.")]
    public string Status { get; set; } = "Plan to Watch";
    
    [MaxLength(400)]
    public string? ImageUrl { get; set; }
    
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateUpdated { get; set; }
}