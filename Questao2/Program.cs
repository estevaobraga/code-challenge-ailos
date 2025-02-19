﻿using System.Threading.Tasks;
using Newtonsoft.Json;
using Questao2.ApiClients;
using Questao2.Services;

public class Program
{
    public static async Task Main()
    {
        var httpClient = new HttpClient();
        var matchApiClient = new MatchApiClient(httpClient);
        var matchService = new MatchService(matchApiClient);

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await matchService.GetTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await matchService.GetTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

}