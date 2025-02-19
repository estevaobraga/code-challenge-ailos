using System;

namespace Questao5.Application.Queries.Responses;

public class GetCurrentAccountBalanceResponse
{
    public int CurrentAccountNumber { get; set; }
    public string? HolderName { get; set; }
    public string? QueryDate { get; set; }
    public decimal Balance { get; set; }
}
