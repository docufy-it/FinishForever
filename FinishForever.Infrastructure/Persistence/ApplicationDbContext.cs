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
public DbSet<ApplicationUser> ApplicationUsers
    => Set<ApplicationUser>();

public DbSet<StravaAthlete> StravaAthletes
    => Set<StravaAthlete>();

public DbSet<StravaActivity> StravaActivities
    => Set<StravaActivity>();

        protected override void OnModelCreating(
    ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<ApplicationUser>(entity =>
    {
        entity.ToTable("ApplicationUsers");

        entity.HasOne(x => x.StravaAthlete)
              .WithOne(x => x.User)
              .HasForeignKey<StravaAthlete>(x => x.UserId);
    });

    builder.Entity<StravaAthlete>(entity =>
    {
        entity.ToTable("StravaAthletes");

        entity.HasIndex(x => x.StravaId)
              .IsUnique();

        entity.HasMany(x => x.Activities)
              .WithOne(x => x.Athlete)
              .HasForeignKey(x => x.AthleteId);
    });

    builder.Entity<StravaActivity>(entity =>
    {
        entity.ToTable("StravaActivities");

        entity.HasIndex(x => x.StravaActivityId)
              .IsUnique();
    });

    builder.Entity<StravaActivity>()
    .HasIndex(x => x.AthleteId);

    builder.Entity<StravaActivity>()
    .HasIndex(x => x.StartDate);
        
}

}