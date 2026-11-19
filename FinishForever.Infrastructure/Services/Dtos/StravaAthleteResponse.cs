using System.Text.Json.Serialization;

namespace FinishForever.Infrastructure.Services.Dtos;

public class StravaAthleteResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("profile")]
    public string? Profile { get; set; }

    [JsonPropertyName("profile_medium")]
    public string? ProfileMedium { get; set; }

    [JsonPropertyName("premium")]
    public bool Premium { get; set; }

    [JsonPropertyName("summit")]
    public bool Summit { get; set; }
}