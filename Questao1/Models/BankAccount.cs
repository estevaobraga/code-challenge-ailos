using System;
using Questao1.Services;

namespace Questao1.Models;

public class BankAccount
{
    public int AccountNumber { get; }
    public string HolderName { get; set; }
    public double Balance { get; private set; }

    public BankAccount(int accountNumber, string holderName, double initialBalance = 0)
    {
        AccountNumber = accountNumber;
        HolderName = holderName;
        Balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");

        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be greater than zero.");

        Balance -= (amount + AccountService.WithdrawalFee);
    }
}
