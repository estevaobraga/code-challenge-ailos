using System;

namespace Questao2.ResponseModels;

public class Match
{
    public string? Competition { get; set; }
    public int Year { get; set; }
    public string? Round { get; set; }
    public string? Team1 { get; set; }
    public string? Team2 { get; set; }
    public string? Team1goals { get; set; }
    public string? Team2goals { get; set; }
}
