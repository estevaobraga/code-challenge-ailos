using System;

namespace Questao5.Domain.Entities;

public class CurrentAccount
{
    public string? Id { get; set; }
    public int AccountNumber { get; set; }
    public string? HolderName { get; set; }
    public bool Active { get; set; }
}
