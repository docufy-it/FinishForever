namespace FinishForever.Application.Models;

public class AnnualDashboard
{
    public int Year { get; set; }

    public int TotalActivities { get; set; }

    public double TotalKm { get; set; }

    public double TotalHours { get; set; }

    public double TotalElevation { get; set; }

    public double LongestRunKm { get; set; }

    public double AverageRunKm { get; set; }

    public string BestMonth { get; set; } = string.Empty;

    public DateTime FirstActivityDate { get; set; }

    public DateTime LastActivityDate { get; set; }

    public int ActiveDays { get; set; }

    public int ActiveMonths { get; set; }

    public string Insight { get; set; } = string.Empty;

    public int LongestStreak { get; set; }

    public double BestMonthKm { get; set; }

    public double BestMonthPercentage { get; set; }

    public int TotalKudos { get; set; }

    public double MarathonEquivalent { get; set; }

    public DateTime BestDayDate { get; set; }

    public double BestDayDistance { get; set; }
}