using System;
using System.Text.Json.Serialization;

namespace Questao2.ResponseModels;

public class ApiResponse
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<Match>? Data { get; set; }
}
