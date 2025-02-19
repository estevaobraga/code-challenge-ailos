using System;

namespace Questao5.Domain.Entities;

public class AccountMoviment
{
    public string? Id { get; set; }
    public string? CurrentAccountId { get; set; }
    public string? Date { get; set; }
    public string? MovimentType { get; set; }
    public decimal Value { get; set; }
}
