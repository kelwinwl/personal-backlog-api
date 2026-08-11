using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Models;

namespace PersonalBacklog.Api.Data;

public class BacklogDbContext : DbContext
{
    public BacklogDbContext(DbContextOptions<BacklogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Anime> Animes { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Anime>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateAdded = DateTime.UtcNow;
                entry.Entity.DateUpdated = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateUpdated =  DateTime.UtcNow;
                
                entry.Property(anime => anime.DateAdded).IsModified = false;
            }
                
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}