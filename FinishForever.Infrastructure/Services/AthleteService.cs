using FinishForever.Domain.Entities;
using FinishForever.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinishForever.Infrastructure.Services;

public class AthleteService
{
    private readonly ApplicationDbContext _db;

    public AthleteService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<StravaAthlete> GetOrCreateAsync(
        long stravaId,
        string userName,
        string firstName,
        string lastName,
        string? profilePicture,
        bool premium,
        bool summit,
        string accessToken,
        string refreshToken,
        DateTime expiresAt)
    {
        var athlete =
            await _db.StravaAthletes
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.StravaId == stravaId);

        if (athlete != null)
        {
            athlete.AccessToken = accessToken;
            athlete.RefreshToken = refreshToken;
            athlete.TokenExpiresAt = expiresAt;
            athlete.LastSyncAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return athlete;
        }

        var user = new ApplicationUser
        {
            Email = "",
            DisplayName =
                $"{firstName} {lastName}"
        };

        athlete = new StravaAthlete
        {
            User = user,
            StravaId = stravaId,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            ProfilePictureUrl = profilePicture,
            Premium = premium,
            Summit = summit,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenExpiresAt = expiresAt,
            LastSyncAt = DateTime.UtcNow
        };

        _db.StravaAthletes.Add(athlete);

        await _db.SaveChangesAsync();

        return athlete;
    }
}