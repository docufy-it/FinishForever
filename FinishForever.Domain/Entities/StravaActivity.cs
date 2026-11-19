using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinishForever.Domain.Entities;

public class StravaActivity
{
    [Key]
[DatabaseGenerated(DatabaseGeneratedOption.None)]
public long Id { get; set; }

[JsonIgnore]
public Guid AthleteId { get; set; }

[JsonIgnore]
public StravaAthlete Athlete { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public double Distance { get; set; }

    [JsonPropertyName("moving_time")]
    public int MovingTime { get; set; }

    [JsonPropertyName("total_elevation_gain")]
    public double TotalElevationGain { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("start_date_local")]
    public DateTime StartDateLocal { get; set; }
}