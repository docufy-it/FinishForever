    namespace FinishForever.Application.Models;

public class MonthlyBreakdown
{
    public int Month { get; set; }

    public string MonthName { get; set; } = string.Empty;

    public int Activities { get; set; }

    public double Km { get; set; }

    public double Hours { get; set; }

    public double Elevation { get; set; }
}