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
            int year)
    {
        var activities =
            await _db.StravaActivities
                .Where(x =>
                    x.Athlete.UserId == userId &&
                    x.StartDate.Year == year)
                .ToListAsync();

        if (!activities.Any())
        {
            return new AnnualDashboard
            {
                Year = year
            };
        }

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
    insight
            
        };
    }

    public async Task<List<MonthlyBreakdown>>
        GetMonthlyBreakdownAsync(
            Guid userId,
            int year)
    {
        var activities =
            await _db.StravaActivities
                .Where(x =>
                    x.Athlete.UserId == userId &&
                    x.StartDate.Year == year)
                .ToListAsync();

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
        int year)
{
    return new YearDashboardViewModel
    {
        Summary =
            await GetAnnualDashboardAsync(
                userId,
                year),

        Months =
            await GetMonthlyBreakdownAsync(
                userId,
                year),

        LatestActivities =
            await GetLatestActivitiesAsync(
                userId)
    };
}
}