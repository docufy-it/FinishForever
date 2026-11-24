using FinishForever.Domain.Entities;

namespace FinishForever.Application.Models;

public class YearDashboardViewModel
{
    public AnnualDashboard Summary { get; set; }
        = new();

    public List<MonthlyBreakdown> Months { get; set; }
        = new();

    public List<StravaActivity> TopKudosActivities { get; set; }
        = new();

    public List<int> AvailableYears { get; set; }
= [];

    public List<SportFilterItem> AvailableSports { get; set; }
    = [];

    public List<HeatmapDay> Heatmap { get; set; }
= [];


}