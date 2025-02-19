using System;
using System.Text.Json;
using Questao2.ResponseModels;

namespace Questao2.ApiClients;

public class MatchApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _basePath;

    public MatchApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _basePath = "https://jsonmock.hackerrank.com/api/football_matches";
    }

    public async Task<ApiResponse> GetMatchesAsync(string team, int year, int page, bool isTeam1 = true)
    {
        var url = $"{_basePath}?year={year}&{(isTeam1 ? "team1" : "team2")}={team}&page={page}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse>(content, jsonOptions());
    }

    private JsonSerializerOptions jsonOptions()
    {
        return new JsonSerializerOptions{
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        };
    }
}

internal class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
}
