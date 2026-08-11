using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Models;

namespace PersonalBacklog.Api.Data;

public class BacklogDbContext : DbContext
{
    public BacklogDbContext(DbContextOptions<BacklogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Anime> Animes { get; set; }
}