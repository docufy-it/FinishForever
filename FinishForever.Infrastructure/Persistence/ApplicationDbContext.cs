using Microsoft.EntityFrameworkCore;
using FinishForever.Domain.Entities;

namespace FinishForever.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users
    => Set<ApplicationUser>();

public DbSet<StravaAthlete> StravaAthletes
    => Set<StravaAthlete>();

public DbSet<StravaActivity> Activities
    => Set<StravaActivity>();

        
        protected override void OnModelCreating(
    ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<ApplicationUser>()
        .HasOne(x => x.StravaAthlete)
        .WithOne(x => x.User)
        .HasForeignKey<StravaAthlete>(
            x => x.UserId);

    builder.Entity<StravaAthlete>()
        .HasMany(x => x.Activities)
        .WithOne(x => x.Athlete)
        .HasForeignKey(x => x.AthleteId);

    builder.Entity<StravaAthlete>()
        .HasIndex(x => x.StravaId)
        .IsUnique();
}

}