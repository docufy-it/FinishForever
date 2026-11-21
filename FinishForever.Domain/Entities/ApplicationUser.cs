using FinishForever.Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime LastLoginAt { get; set; }

    public DateTime? LastActivitySyncAt { get; set; }

    public StravaAthlete? StravaAthlete { get; set; }
}