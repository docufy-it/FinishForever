using System.Net.Http.Headers;
using System.Net.Http.Json;
using FinishForever.Infrastructure.Configuration;
using FinishForever.Infrastructure.Services.Dtos;
using Microsoft.Extensions.Options;

namespace FinishForever.Infrastructure.Services;

public class StravaApiClient
{
    private readonly HttpClient _httpClient;
    private readonly StravaOptions _options;

    public StravaApiClient(
        HttpClient httpClient,
        IOptions<StravaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public string GetAuthorizationUrl()
    {
        return
            $"https://www.strava.com/oauth/authorize" +
            $"?client_id={_options.ClientId}" +
            $"&redirect_uri={Uri.EscapeDataString(_options.RedirectUri)}" +
            $"&response_type=code" +
            $"&approval_prompt=force" +
            $"&scope=read,activity:read_all";
    }

    public async Task<StravaTokenResponse?> ExchangeCodeAsync(
        string code)
    {
        var response = await _httpClient.PostAsync(
            "https://www.strava.com/oauth/token",
            new FormUrlEncodedContent(
            [
                new("client_id", _options.ClientId),
                new("client_secret", _options.ClientSecret),
                new("code", code),
                new("grant_type", "authorization_code")
            ]));

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<StravaTokenResponse>();
    }

    public async Task<StravaAthleteResponse?> GetAthleteAsync(
        string accessToken)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "https://www.strava.com/api/v3/athlete");

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                accessToken);

        var response =
            await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<StravaAthleteResponse>();
    }

    public async Task<List<StravaActivityResponse>>
        GetActivitiesAsync(
            string accessToken,
            int page,
            int perPage = 200)
    {
        var request =
            new HttpRequestMessage(
                HttpMethod.Get,
                $"https://www.strava.com/api/v3/athlete/activities?page={page}&per_page={perPage}");

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                accessToken);

        var response =
            await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await response.Content
                   .ReadFromJsonAsync<
                       List<StravaActivityResponse>>()
               ?? new();
    }

    public async Task<List<StravaActivityResponse>>
        GetAllActivitiesAsync(
            string accessToken)
    {
        var result =
            new List<StravaActivityResponse>();

        var page = 1;

        while (true)
        {
            var batch =
                await GetActivitiesAsync(
                    accessToken,
                    page);

            if (batch.Count == 0)
            {
                break;
            }

            result.AddRange(batch);

            page++;
        }

        return result;
    }

    public async Task<List<StravaActivityResponse>>
    GetActivitiesAfterAsync(
        string accessToken,
        DateTime afterDate)
{
    var result =
        new List<StravaActivityResponse>();

    var after =
        ((DateTimeOffset)afterDate)
        .ToUnixTimeSeconds();

    var page = 1;

    while (true)
    {
        var request =
            new HttpRequestMessage(
                HttpMethod.Get,
                $"https://www.strava.com/api/v3/athlete/activities?page={page}&per_page=200&after={after}");

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                accessToken);

        var response =
            await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var batch =
            await response.Content
                .ReadFromJsonAsync<
                    List<StravaActivityResponse>>()
            ?? new();

        if (!batch.Any())
        {
            break;
        }

        result.AddRange(batch);

        page++;
    }

    return result;
}
}