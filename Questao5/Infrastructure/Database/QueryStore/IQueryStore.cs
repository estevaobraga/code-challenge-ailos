using System;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore;

public interface IQueryStore
{
    Task<CurrentAccount> GetCurrentAccountById(string currentAccountId);
    Task<decimal> GetCurrentAccountBalance(string currentAccountId);
}
