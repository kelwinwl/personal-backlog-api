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
                
                // Prevents the original creation date from being overwritten to 01-01-0001 during update request
                entry.Property(a => a.DateAdded).IsModified = false;
            }
                
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}