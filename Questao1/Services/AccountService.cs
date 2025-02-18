using System;
using System.Threading.Tasks;
using Questao1.Models;
using Questao1.Repositories.Interfaces;
using Questao1.Services.Interfaces;

namespace Questao1.Services;

public class AccountService : IAccountService
{
    public const double WithdrawalFee = 3.50;

    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task CreateAccount(int accountNumber, string holderName, double initialDeposit = 0)
    {
        var account = new BankAccount(accountNumber, holderName, initialDeposit);
        await _accountRepository.AddAccount(account);
    }

    public async Task Deposit(int accountNumber, double amount)
    {
        var account = await _accountRepository.GetAccount(accountNumber);
        account.Deposit(amount);
        await _accountRepository.UpdateAccount(account);
    }

    public async Task Withdraw(int accountNumber, double amount)
    {
        var account = await _accountRepository.GetAccount(accountNumber);
        account.Withdraw(amount);
        await _accountRepository.UpdateAccount(account);
    }

    public async Task<BankAccount> GetAccountDetails(int accountNumber)
    {
        return await _accountRepository.GetAccount(accountNumber);
    }
}
