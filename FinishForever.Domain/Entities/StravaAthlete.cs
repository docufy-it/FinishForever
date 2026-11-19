namespace FinishForever.Domain.Entities;

public class StravaAthlete
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public long StravaId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; }

    public bool Premium { get; set; }

    public bool Summit { get; set; }

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime TokenExpiresAt { get; set; }

    public DateTime? LastSyncAt { get; set; }

    public ICollection<StravaActivity> Activities
        = new List<StravaActivity>();
}