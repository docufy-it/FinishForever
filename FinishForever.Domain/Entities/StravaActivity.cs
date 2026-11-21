namespace FinishForever.Domain.Entities;

public class StravaActivity
{
    public Guid Id { get; set; }

    public Guid AthleteId { get; set; }

    public StravaAthlete Athlete { get; set; } = null!;

    // Strava identifiers
    public long StravaActivityId { get; set; }

    public long? ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    // Activity info
    public string SportType { get; set; } = string.Empty;

    public string ActivityType { get; set; } = string.Empty;

    public bool IsRace { get; set; }

    public bool IsTrainer { get; set; }

    public bool IsCommute { get; set; }

    public bool IsManual { get; set; }

    public bool IsPrivate { get; set; }

    // Distance & elevation
    public double DistanceMeters { get; set; }

    public double DistanceKm { get; set; }

    public double TotalElevationGain { get; set; }

    public double ElevationHigh { get; set; }

    public double ElevationLow { get; set; }

    // Timing
    public DateTime StartDate { get; set; }

    public DateTime StartDateLocal { get; set; }

    public int MovingTimeSeconds { get; set; }

    public int ElapsedTimeSeconds { get; set; }

    // Speed
    public double AverageSpeed { get; set; }

    public double MaxSpeed { get; set; }

    public double AverageCadence { get; set; }

    // Heart Rate
    public bool HasHeartRate { get; set; }

    public double AverageHeartRate { get; set; }

    public double MaxHeartRate { get; set; }

    // Power
    public bool HasPower { get; set; }

    public double AverageWatts { get; set; }

    public double MaxWatts { get; set; }

    public double WeightedAverageWatts { get; set; }

    // Calories
    public double Calories { get; set; }

    // Achievement counters
    public int AchievementCount { get; set; }

    public int KudosCount { get; set; }

    public int CommentCount { get; set; }

    public int PhotoCount { get; set; }

    public int TotalPhotoCount { get; set; }

    // Device info
    public string DeviceName { get; set; } = string.Empty;

    public string GearId { get; set; } = string.Empty;

    // GPS
    public string TimeZone { get; set; } = string.Empty;

    public string LocationCity { get; set; } = string.Empty;

    public string LocationState { get; set; } = string.Empty;

    public string LocationCountry { get; set; } = string.Empty;

    // Analytics
    public double SufferScore { get; set; }

    public double RelativeEffort { get; set; }


public double? StartLatitude { get; set; }

public double? StartLongitude { get; set; }

public double? EndLatitude { get; set; }

public double? EndLongitude { get; set; }

public double PaceSecondsPerKm { get; set; }

public double AverageSpeedKmH { get; set; }

public double MaxSpeedKmH { get; set; }

public bool IsPersonalBest { get; set; }

public bool IsLongestRun { get; set; }

public bool IsFastestRun { get; set; }

public string Description { get; set; } = string.Empty;

public string Polyline { get; set; } = string.Empty;
    // Sync
    public DateTime ImportedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime LastUpdatedAt { get; set; }
        = DateTime.UtcNow;
}