using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Models;

namespace PersonalBacklog.Api.Data;

public class BacklogDbContext : DbContext
{
    public BacklogDbContext(DbContextOptions<BacklogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Anime> Animes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAnime> UserAnimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAnime>()
            .HasKey(ua => new { ua.UserId, ua.AnimeId });
        
        modelBuilder.Entity<UserAnime>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.UserAnimes)
            .HasForeignKey(ua => ua.UserId);
        
        modelBuilder.Entity<UserAnime>()
            .HasOne(ua => ua.Anime)
            .WithMany(a => a.UserAnimes)
            .HasForeignKey(ua => ua.AnimeId);

        modelBuilder.Entity<Anime>()
            .HasIndex(a => a.MalId)
            .IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Anime>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateUpdated = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateUpdated =  DateTime.UtcNow;
            }
        }
        
        
        return base.SaveChangesAsync(cancellationToken);
    }
}