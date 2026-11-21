using FinishForever.Domain.Entities;

namespace FinishForever.Application.Models;

public class YearDashboardViewModel
{
    public AnnualDashboard Summary { get; set; }
        = new();

    public List<MonthlyBreakdown> Months { get; set; }
        = new();

    public List<StravaActivity> LatestActivities { get; set; }
        = new();
}