using FinishForever.Domain.Entities;
using FinishForever.Infrastructure.Persistence;
using FinishForever.Infrastructure.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FinishForever.Infrastructure.Services;

public class StravaActivityService
{
    private readonly ApplicationDbContext _db;
    private readonly StravaApiClient _stravaApi;

    public StravaActivityService(
        ApplicationDbContext db,
        StravaApiClient stravaApi)
    {
        _db = db;
        _stravaApi = stravaApi;
    }

    public async Task<int> SyncAsync(
        Guid athleteId)
    {
        var athlete =
            await _db.StravaAthletes
                .FirstOrDefaultAsync(
                    x => x.Id == athleteId);

        if (athlete == null)
        {
            return 0;
        }

        var lastActivity =
    await _db.StravaActivities
        .Where(x =>
            x.AthleteId == athleteId)
        .OrderByDescending(x =>
            x.StartDate)
        .FirstOrDefaultAsync();

List<StravaActivityResponse> activities;

if (lastActivity == null)
{
    activities =
        await _stravaApi.GetAllActivitiesAsync(
            athlete.AccessToken);
}
else
{
    activities =
        await _stravaApi.GetActivitiesAfterAsync(
            athlete.AccessToken,
            lastActivity.StartDate);
}

        foreach (var activity in activities)
        {
            var existingActivity =
                await _db.StravaActivities
                    .FirstOrDefaultAsync(
                        x => x.StravaActivityId ==
                             activity.Id);

            if (existingActivity != null)
            {
                continue;
            }

            var entity =
                MapActivity(
                    athlete.Id,
                    activity);

            _db.StravaActivities.Add(
                entity);
        }

        await _db.SaveChangesAsync();

        athlete.LastSyncAt =
            DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return activities.Count;
    }

    private static StravaActivity MapActivity(
        Guid athleteId,
        StravaActivityResponse activity)
    {
        return new StravaActivity
        {
            AthleteId = athleteId,

            StravaActivityId = activity.Id,

            Name = activity.Name,

            SportType =
                activity.SportType,

            ActivityType =
                activity.Type,

            IsTrainer =
                activity.IsTrainer,

            IsCommute =
                activity.IsCommute,

            IsManual =
                activity.IsManual,

            IsPrivate =
                activity.IsPrivate,

            DistanceMeters =
                activity.DistanceMeters,

            DistanceKm =
                activity.DistanceMeters / 1000,

            TotalElevationGain =
                activity.TotalElevationGain,

            ElevationHigh =
                activity.ElevationHigh ?? 0,

            ElevationLow =
                activity.ElevationLow ?? 0,

            StartDate =
                activity.StartDate,

            StartDateLocal =
                activity.StartDateLocal,

            MovingTimeSeconds =
                activity.MovingTimeSeconds,

            ElapsedTimeSeconds =
                activity.ElapsedTimeSeconds,

            AverageSpeed =
                activity.AverageSpeed,

            MaxSpeed =
                activity.MaxSpeed,

            AverageCadence =
                activity.AverageCadence ?? 0,

            HasHeartRate =
                activity.HasHeartRate,

            AverageHeartRate =
                activity.AverageHeartRate ?? 0,

            MaxHeartRate =
                activity.MaxHeartRate ?? 0,

            AverageWatts =
                activity.AverageWatts ?? 0,

            MaxWatts =
                activity.MaxWatts ?? 0,

            WeightedAverageWatts =
                activity.WeightedAverageWatts ?? 0,

            Calories =
                activity.Calories ?? 0,

            AchievementCount =
                activity.AchievementCount,

            KudosCount =
                activity.KudosCount,

            CommentCount =
                activity.CommentCount,

            PhotoCount =
                activity.PhotoCount,

            TotalPhotoCount =
                activity.TotalPhotoCount,

            DeviceName =
                activity.DeviceName ?? "",

            GearId =
                activity.GearId ?? "",

            TimeZone =
                activity.TimeZone ?? "",

            LocationCity =
                activity.LocationCity ?? "",

            LocationState =
                activity.LocationState ?? "",

            LocationCountry =
                activity.LocationCountry ?? "",

            SufferScore =
                activity.SufferScore ?? 0,

            Description =
                activity.Description ?? "",

            Polyline =
                activity.Map?.SummaryPolyline ?? "",

            ImportedAt =
                DateTime.UtcNow,

            LastUpdatedAt =
                DateTime.UtcNow
        };
    }
}