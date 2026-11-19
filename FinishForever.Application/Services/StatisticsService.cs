using FinishForever.Domain.Entities;
using FinishForever.Domain.Models;

namespace FinishForever.Application.Services;

public class StatisticsService
{
    public YearStatistics Calculate(
        IEnumerable<StravaActivity> activities)
    {
        var runs = activities
            .Where(x => x.Type == "Run")
            .ToList();

        return new YearStatistics
        {
            TotalActivities = runs.Count,

            TotalKm =
                runs.Sum(x => x.Distance) / 1000,

            TotalElevation =
                runs.Sum(x => x.TotalElevationGain),

            TotalHours =
                runs.Sum(x => x.MovingTime) / 3600.0,

            LongestRunKm =
                runs.Any()
                    ? runs.Max(x => x.Distance) / 1000
                    : 0,

            MarathonCount =
                runs.Count(x => x.Distance >= 42195)
        };
    }
}