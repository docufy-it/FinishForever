using FinishForever.Application.Models;
using FinishForever.Domain.Entities;
using FinishForever.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FinishForever.Infrastructure.Services;

public class DashboardService
{
    private readonly ApplicationDbContext _db;

    public DashboardService(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AnnualDashboard>
        GetAnnualDashboardAsync(
            Guid userId,
            int year,
            string? sportType = null)
    {
        var query =
    _db.StravaActivities
        .Where(x =>
            x.Athlete.UserId == userId &&
            x.StartDate.Year == year);

        if (!string.IsNullOrWhiteSpace(sportType))
        {
            query =
                query.Where(x =>
                    x.SportType == sportType);
        }

        var activities =
            await query.ToListAsync();

        if (!activities.Any())
        {
            return new AnnualDashboard
            {
                Year = year
            };
        }

        var activeDates =
            activities
                .Select(x => x.StartDate.Date)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

        var longestStreak = 0;
        var currentStreak = 1;

        for (int i = 1; i < activeDates.Count; i++)
        {
            if (activeDates[i] ==
                activeDates[i - 1].AddDays(1))
            {
                currentStreak++;
            }
            else
            {
                longestStreak =
                    Math.Max(
                        longestStreak,
                        currentStreak);

                currentStreak = 1;
            }
        }

        longestStreak =
            Math.Max(
                longestStreak,
                currentStreak);

        var bestMonth =
            activities
                .GroupBy(x => x.StartDate.Month)
                .Select(x => new
                {
                    Month = x.Key,
                    Km = x.Sum(a => a.DistanceKm)
                })
                .OrderByDescending(x => x.Km)
                .First();

        var bestDay =
            activities
                .GroupBy(x => x.StartDate.Date)
                .Select(x => new
                {
                    Date = x.Key,
                    Distance = x.Sum(a => a.DistanceKm)
                })
                .OrderByDescending(x => x.Distance)
                .FirstOrDefault();

        var totalKm =
            activities.Sum(x => x.DistanceKm);


        var activeDays =
            activities
                .Select(x => x.StartDate.Date)
                .Distinct()
                .Count();

        var activeMonths =
            activities
                .Select(x => x.StartDate.Month)
                .Distinct()
                .Count();

        var insight =
            $"You covered {activities.Sum(x => x.DistanceKm):N0} km across {activities.Count} activities.";

        return new AnnualDashboard
        {
            Year = year,

            TotalActivities =
                activities.Count,

            TotalKm =
                activities.Sum(x => x.DistanceKm),

            TotalHours =
                activities.Sum(x => x.MovingTimeSeconds) / 3600d,

            TotalElevation =
                activities.Sum(x => x.TotalElevationGain),

            LongestRunKm =
                activities.Max(x => x.DistanceKm),

            AverageRunKm =
                activities.Average(x => x.DistanceKm),

            FirstActivityDate =
                activities.Min(x => x.StartDate),

            LastActivityDate =
                activities.Max(x => x.StartDate),

            BestMonth =
                CultureInfo
                    .CurrentCulture
                    .DateTimeFormat
                    .GetMonthName(
                        bestMonth.Month),

            ActiveDays =
    activeDays,

            ActiveMonths =
    activeMonths,

            Insight =
    insight,

            LongestStreak = longestStreak,

            BestMonthKm =
    bestMonth.Km,

            BestMonthPercentage =
    totalKm == 0
        ? 0
        : bestMonth.Km / totalKm * 100,

            TotalKudos =
    activities.Sum(x => x.KudosCount),

            MarathonEquivalent =
    activities.Sum(x => x.DistanceKm) / 42.195,

            BestDayDate =
    bestDay?.Date ?? DateTime.MinValue,

            BestDayDistance =
    bestDay?.Distance ?? 0,

        };
    }

    public async Task<List<MonthlyBreakdown>>
        GetMonthlyBreakdownAsync(
            Guid userId,
            int year,
            string? sportType = null)
    {
        var query =
    _db.StravaActivities
        .Where(x =>
            x.Athlete.UserId == userId &&
            x.StartDate.Year == year);

        if (!string.IsNullOrWhiteSpace(sportType))
        {
            query =
                query.Where(x =>
                    x.SportType == sportType);
        }

        var activities =
            await query.ToListAsync();

        return activities
            .GroupBy(x => x.StartDate.Month)
            .OrderBy(x => x.Key)
            .Select(x => new MonthlyBreakdown
            {
                Month = x.Key,

                MonthName =
    CultureInfo.CurrentCulture.TextInfo
        .ToTitleCase(
            CultureInfo.CurrentCulture
                .DateTimeFormat
                .GetMonthName(x.Key)),

                Activities =
                    x.Count(),

                Km =
                    x.Sum(a => a.DistanceKm),

                Hours =
                    x.Sum(a =>
                        a.MovingTimeSeconds) / 3600d,

                Elevation =
                    x.Sum(a =>
                        a.TotalElevationGain)
            })
            .ToList();
    }

    public async Task<List<StravaActivity>>
        GetLatestActivitiesAsync(
            Guid userId,
            int take = 10)
    {
        return await _db.StravaActivities
            .Where(x =>
                x.Athlete.UserId == userId)
            .OrderByDescending(x =>
                x.StartDate)
            .Take(take)
            .ToListAsync();
    }

    public async Task<YearDashboardViewModel>
    GetYearDashboardAsync(
        Guid userId,
        int year,
        string? sportType = null)
    {
        return new YearDashboardViewModel
        {
            Summary =
                await GetAnnualDashboardAsync(
                    userId,
                    year,
                    sportType),

            Months =
                await GetMonthlyBreakdownAsync(
                    userId,
                    year,
                    sportType),

            TopKudosActivities =
        await GetTopKudosActivitiesAsync(
            userId,
            year,
            sportType),


            AvailableYears =
            await GetAvailableYearsAsync(
                userId),

            AvailableSports =
    await GetAvailableSportsAsync(
        userId, year),

            Heatmap =
    await GetHeatmapAsync(
        userId,
        year,
        sportType)

        };



    }

    public async Task<List<StravaActivity>>
        GetTopKudosActivitiesAsync(
            Guid userId,
            int year,
            string? sportType = null,
            int take = 5)
    {
        var query =
            _db.StravaActivities
                .Where(x =>
                    x.Athlete.UserId == userId &&
                    x.StartDate.Year == year);

        if (!string.IsNullOrWhiteSpace(sportType))
        {
            query =
                query.Where(x =>
                    x.SportType == sportType);
        }

        return await query
            .OrderByDescending(x => x.KudosCount)
            .ThenByDescending(x => x.StartDate)
            .Take(take)
            .ToListAsync();
    }

    public async Task<List<int>>
    GetAvailableYearsAsync(
        Guid userId)
    {
        return await _db.StravaActivities
            .Where(x =>
                x.Athlete.UserId == userId)
            .Select(x =>
                x.StartDate.Year)
            .Distinct()
            .OrderByDescending(x =>
                x)
            .ToListAsync();
    }

    public async Task<List<SportFilterItem>>
    GetAvailableSportsAsync(
        Guid userId,
        int year)
    {
        return await _db.StravaActivities
            .Where(x =>
                x.Athlete.UserId == userId &&
                x.StartDate.Year == year)
            .GroupBy(x =>
                x.SportType)
            .Select(x =>
                new SportFilterItem
                {
                    SportType = x.Key,
                    Count = x.Count()
                })
            .OrderByDescending(x =>
                x.Count)
            .ToListAsync();
    }

    public async Task<List<HeatmapDay>>
    GetHeatmapAsync(
        Guid userId,
        int year,
        string? sportType = null)
    {
        IQueryable<StravaActivity> query =
            _db.StravaActivities
                .Where(x =>
                    x.Athlete.UserId == userId &&
                    x.StartDate.Year == year);

        if (!string.IsNullOrWhiteSpace(sportType))
        {
            query =
                query.Where(x =>
                    x.SportType == sportType);
        }

        var activities =
            await query.ToListAsync();

        var activityDays =
            activities
                .GroupBy(x => x.StartDate.Date)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        DistanceKm =
                            g.Sum(x => x.DistanceKm),

                        Activities =
                            g.Count()
                    });

        var firstDayOfYear =
            new DateTime(year, 1, 1);

        var lastDayOfYear =
            new DateTime(year, 12, 31);

        // Lunedì della settimana che contiene il 1 gennaio

        var firstHeatmapDay =
            firstDayOfYear.AddDays(
                -(((int)firstDayOfYear.DayOfWeek + 6) % 7));

        // Domenica della settimana che contiene il 31 dicembre

        var lastHeatmapDay =
            lastDayOfYear.AddDays(
                6 - (((int)lastDayOfYear.DayOfWeek + 6) % 7));

        var result =
            new List<HeatmapDay>();

        for (var day = firstHeatmapDay;
             day <= lastHeatmapDay;
             day = day.AddDays(1))
        {
            activityDays.TryGetValue(
                day.Date,
                out var dayData);

            var km =
                dayData?.DistanceKm ?? 0;

            var activityCount =
                dayData?.Activities ?? 0;

            var week =
                (int)((day - firstHeatmapDay)
                    .TotalDays / 7);

            var dayIndex =
                ((int)day.DayOfWeek + 6) % 7;

            result.Add(
                new HeatmapDay
                {
                    Date = day,

                    DistanceKm = km,

                    Activities = activityCount,

                    WeekOfYear = week,

                    DayOfWeekIndex = dayIndex,

                    Level = km switch
                    {
                        <= 0 => 0,
                        < 5 => 1,
                        < 10 => 2,
                        < 20 => 3,
                        _ => 4
                    }
                });
        }

        return result;
    }
}