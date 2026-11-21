using System.Text.Json.Serialization;

namespace FinishForever.Infrastructure.Services.Dtos;

public class StravaActivityResponse
{
    // Identifiers

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("upload_id")]
    public long? UploadId { get; set; }

    [JsonPropertyName("resource_state")]
    public int ResourceState { get; set; }

    // General

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("sport_type")]
    public string SportType { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("workout_type")]
    public int? WorkoutType { get; set; }

    // Flags

    [JsonPropertyName("private")]
    public bool IsPrivate { get; set; }

    [JsonPropertyName("trainer")]
    public bool IsTrainer { get; set; }

    [JsonPropertyName("commute")]
    public bool IsCommute { get; set; }

    [JsonPropertyName("manual")]
    public bool IsManual { get; set; }

    [JsonPropertyName("flagged")]
    public bool Flagged { get; set; }

    [JsonPropertyName("has_kudoed")]
    public bool HasKudoed { get; set; }

    // Dates

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("start_date_local")]
    public DateTime StartDateLocal { get; set; }

    [JsonPropertyName("timezone")]
    public string? TimeZone { get; set; }

    [JsonPropertyName("utc_offset")]
    public double UtcOffset { get; set; }

    // Geography

    [JsonPropertyName("location_city")]
    public string? LocationCity { get; set; }

    [JsonPropertyName("location_state")]
    public string? LocationState { get; set; }

    [JsonPropertyName("location_country")]
    public string? LocationCountry { get; set; }

    [JsonPropertyName("start_latlng")]
    public float[]? StartLatLng { get; set; }

    [JsonPropertyName("end_latlng")]
    public float[]? EndLatLng { get; set; }

    // Distance

    [JsonPropertyName("distance")]
    public double DistanceMeters { get; set; }

    [JsonPropertyName("total_elevation_gain")]
    public double TotalElevationGain { get; set; }

    [JsonPropertyName("elev_high")]
    public double? ElevationHigh { get; set; }

    [JsonPropertyName("elev_low")]
    public double? ElevationLow { get; set; }

    // Timing

    [JsonPropertyName("moving_time")]
    public int MovingTimeSeconds { get; set; }

    [JsonPropertyName("elapsed_time")]
    public int ElapsedTimeSeconds { get; set; }

    // Speed & Pace

    [JsonPropertyName("average_speed")]
    public double AverageSpeed { get; set; }

    [JsonPropertyName("max_speed")]
    public double MaxSpeed { get; set; }

    [JsonPropertyName("average_cadence")]
    public double? AverageCadence { get; set; }

    // Heart Rate

    [JsonPropertyName("has_heartrate")]
    public bool HasHeartRate { get; set; }

    [JsonPropertyName("average_heartrate")]
    public double? AverageHeartRate { get; set; }

    [JsonPropertyName("max_heartrate")]
    public double? MaxHeartRate { get; set; }

    // Power

    [JsonPropertyName("average_watts")]
    public double? AverageWatts { get; set; }

    [JsonPropertyName("max_watts")]
    public double? MaxWatts { get; set; }

    [JsonPropertyName("weighted_average_watts")]
    public double? WeightedAverageWatts { get; set; }

    [JsonPropertyName("device_watts")]
    public bool DeviceWatts { get; set; }

    [JsonPropertyName("kilojoules")]
    public double? Kilojoules { get; set; }

    // Calories

    [JsonPropertyName("calories")]
    public double? Calories { get; set; }

    // Social

    [JsonPropertyName("achievement_count")]
    public int AchievementCount { get; set; }

    [JsonPropertyName("kudos_count")]
    public int KudosCount { get; set; }

    [JsonPropertyName("comment_count")]
    public int CommentCount { get; set; }

    [JsonPropertyName("athlete_count")]
    public int AthleteCount { get; set; }

    [JsonPropertyName("photo_count")]
    public int PhotoCount { get; set; }

    [JsonPropertyName("total_photo_count")]
    public int TotalPhotoCount { get; set; }

    // Gear

    [JsonPropertyName("gear_id")]
    public string? GearId { get; set; }

    [JsonPropertyName("device_name")]
    public string? DeviceName { get; set; }

    // Analytics

    [JsonPropertyName("suffer_score")]
    public double? SufferScore { get; set; }

    // Description

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    // Map

    [JsonPropertyName("map")]
    public StravaMapResponse? Map { get; set; }
}