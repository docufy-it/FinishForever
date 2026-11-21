using FinishForever.Domain.Entities;
using FinishForever.Infrastructure.Persistence;
using FinishForever.Infrastructure.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FinishForever.Infrastructure.Services;

public class StravaAthleteService
{
    private readonly ApplicationDbContext _db;

    public StravaAthleteService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<StravaAthlete> GetOrCreateAsync(
        StravaAthleteResponse athlete,
        StravaTokenResponse token)
    {
        var existingAthlete =
            await _db.StravaAthletes
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.StravaId == athlete.Id);

        if (existingAthlete != null)
        {
            existingAthlete.UserName =
                athlete.UserName ?? string.Empty;

            existingAthlete.FirstName =
                athlete.FirstName;

            existingAthlete.LastName =
                athlete.LastName;

            existingAthlete.ProfilePictureUrl =
                athlete.Profile;

            existingAthlete.ProfilePictureUrlMedium =
                athlete.ProfileMedium;

            existingAthlete.City =
                athlete.City ?? string.Empty;

            existingAthlete.State =
                athlete.State ?? string.Empty;

            existingAthlete.Country =
                athlete.Country ?? string.Empty;

            existingAthlete.Sex =
                athlete.Sex ?? string.Empty;

            existingAthlete.Weight =
                athlete.Weight;

            existingAthlete.FollowerCount =
                athlete.FollowerCount;

            existingAthlete.FriendCount =
                athlete.FriendCount;

            existingAthlete.MutualFriendCount =
                athlete.MutualFriendCount;

            existingAthlete.Premium =
                athlete.Premium;

            existingAthlete.Summit =
                athlete.Summit;

            existingAthlete.AccessToken =
                token.AccessToken;

            existingAthlete.RefreshToken =
                token.RefreshToken;

            existingAthlete.TokenExpiresAt =
                DateTimeOffset
                    .FromUnixTimeSeconds(
                        token.ExpiresAt)
                    .UtcDateTime;

            existingAthlete.LastSyncAt =
                DateTime.UtcNow;

            existingAthlete.User.LastLoginAt =
                DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return existingAthlete;
        }

        var user = new ApplicationUser
        {
            Email = string.Empty,

            DisplayName =
                $"{athlete.FirstName} {athlete.LastName}",

            CreatedAt =
                DateTime.UtcNow,

            LastLoginAt =
                DateTime.UtcNow
        };

        var newAthlete = new StravaAthlete
        {
            User = user,

            StravaId =
                athlete.Id,

            UserName =
                athlete.UserName ?? string.Empty,

            FirstName =
                athlete.FirstName,

            LastName =
                athlete.LastName,

            ProfilePictureUrl =
                athlete.Profile,

            ProfilePictureUrlMedium =
                athlete.ProfileMedium,

            City =
                athlete.City ?? string.Empty,

            State =
                athlete.State ?? string.Empty,

            Country =
                athlete.Country ?? string.Empty,

            Sex =
                athlete.Sex ?? string.Empty,

            Weight =
                athlete.Weight,

            FollowerCount =
                athlete.FollowerCount,

            FriendCount =
                athlete.FriendCount,

            MutualFriendCount =
                athlete.MutualFriendCount,

            Premium =
                athlete.Premium,

            Summit =
                athlete.Summit,

            AccessToken =
                token.AccessToken,

            RefreshToken =
                token.RefreshToken,

            TokenExpiresAt =
                DateTimeOffset
                    .FromUnixTimeSeconds(
                        token.ExpiresAt)
                    .UtcDateTime,

            FirstSyncAt =
                DateTime.UtcNow,

            LastSyncAt =
                DateTime.UtcNow
        };

        _db.StravaAthletes.Add(
            newAthlete);

        await _db.SaveChangesAsync();

        return newAthlete;
    }
}