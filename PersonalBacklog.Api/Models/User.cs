using System.ComponentModel.DataAnnotations;

namespace PersonalBacklog.Api.Models;

public class User
{
    public int Id { get; set; }
    
    [Length(3, 32, ErrorMessage = "Username must have between 3 and 32 characters.")]
    public required string Username { get; set; }
    
    public required string PasswordHash { get; set; }
    
    public required string Email { get; set; }

    public DateOnly DateUserRegistered { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public ICollection<UserAnime> UserAnimes { get; set; } = new List<UserAnime>();
}
