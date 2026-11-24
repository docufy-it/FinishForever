namespace FinishForever.Application.Models;

public class HeatmapDay
{
    public DateTime Date { get; set; }

    public double DistanceKm { get; set; }

    public int Activities { get; set; }

    public int Level { get; set; }

    public int WeekOfYear { get; set; }

    public int DayOfWeekIndex { get; set; }
}