namespace Shared.Models;

public class WeeklyStatsResponse
{
    public int Passed { get; set; }
    public int Failed { get; set; }
    public int NumberOfGroups { get; set; }
    public double PassedDiff { get; set; }
    public double AverageRunTime { get; set; }
}
