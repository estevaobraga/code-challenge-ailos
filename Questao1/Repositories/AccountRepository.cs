using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Questao1.Models;
using Questao1.Repositories.Interfaces;

namespace Questao1.Repositories;

public class AccountRepository: IAccountRepository
{
    private readonly Dictionary<int, BankAccount> _accounts = new();

    public async Task AddAccount(BankAccount account)
    {
        _accounts[account.AccountNumber] = account;
    }

    public async Task<BankAccount> GetAccount(int accountNumber)
    {
        if (_accounts.TryGetValue(accountNumber, out var account))
            return account;

        throw new KeyNotFoundException("Account not found.");
    }

    public async Task UpdateAccount(BankAccount account)
    {
        if (!_accounts.ContainsKey(account.AccountNumber))
            throw new KeyNotFoundException("Account not found.");

        _accounts[account.AccountNumber] = account;
    }
}
