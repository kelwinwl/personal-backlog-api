namespace PersonalBacklog.Api.Models;

public class Anime
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public int TotalEpisodes { get; set; }
    
    public int CurrentEpisodes { get; set; }
    
    public string Status { get; set; }
    
    public DateTime? DateStarted { get; set; }
    public DateTime? DateFinished { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateUpdated { get; set; }
}