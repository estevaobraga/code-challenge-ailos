using System;
using Questao2.ApiClients;
using Questao2.ResponseModels;

namespace Questao2.Services;

public class MatchService
{
    private readonly MatchApiClient _matchApiClient;

    public MatchService(MatchApiClient matchApiClient)
    {
        _matchApiClient = matchApiClient;
    }

    public async Task<int> GetTotalScoredGoalsAsync(string team, int year)
    {
        int totalGoals = 0;

        totalGoals += await TotalGoalsOfTheYear(team, year);
        totalGoals += await TotalGoalsOfTheYear(team, year, team1: false);

        return totalGoals;
    }

    private async Task<int> TotalGoalsOfTheYear(string team, int year, bool team1 = true)
    {
        int totalGoals = 0;
        int page = 1;
        ApiResponse response;

        do
        {
            response = await _matchApiClient.GetMatchesAsync(team, year, page, team1);
            foreach (var match in response.Data)
            {
                if (match.Team1 == team && int.TryParse(match.Team1goals, out int team1Goals))
                {
                    totalGoals += team1Goals;
                }

                if (match.Team2 == team && int.TryParse(match.Team2goals, out int team2Goals))
                {
                    totalGoals += team2Goals;
                }
            }
            page++;
        } while (page <= response.TotalPages);

        return totalGoals;
    }
}
