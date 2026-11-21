namespace FinishForever.Domain.Entities;

public class StravaAthlete
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    // Strava
    public long StravaId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; }

    public string? ProfilePictureUrlMedium { get; set; }

    // Account
    public bool Premium { get; set; }

    public bool Summit { get; set; }

    public string? ProfileMedium { get; set; }

    public string? ProfileLarge { get; set; }

    // Tokens
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime TokenExpiresAt { get; set; }

    public int ResourceState { get; set; }

    // Profile
    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string Sex { get; set; } = string.Empty;

    public double Weight { get; set; }

    // Activity counters
    public int FollowerCount { get; set; }

    public int FriendCount { get; set; }

    public int MutualFriendCount { get; set; }

    // Sync
    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime? FirstSyncAt { get; set; }

    public DateTime? LastSyncAt { get; set; }

    public ICollection<StravaActivity> Activities
        = new List<StravaActivity>();
}