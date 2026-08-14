namespace PersonalBacklog.Api.Models;

public enum AnimeStatus
{
    PlanToWatch = 0,
    Watching = 1,
    Completed = 2,
    Paused = 3,
    Dropped = 4
}

public class UserAnime
{
    public required int UserId { get; set; }
    public User? User { get; set; }
        
    public required int AnimeId { get; set; }
    public Anime? Anime { get; set; }

    public AnimeStatus Status { get; set; } = AnimeStatus.PlanToWatch;

    public int CurrentEpisodes { get; set; } = 0;

    public DateOnly? DateStarted { get; set; } 
    public DateOnly? DateFinished { get; set; }  
} 
