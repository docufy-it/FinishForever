using System.Text.Json.Serialization;

namespace FinishForever.Infrastructure.Services.Dtos;

public class StravaMapResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("summary_polyline")]
    public string? SummaryPolyline { get; set; }

    [JsonPropertyName("resource_state")]
    public int ResourceState { get; set; }
}