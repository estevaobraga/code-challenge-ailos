using System.Threading.Tasks;
using Questao1.Models;

namespace Questao1.Services.Interfaces;

public interface IAccountService
{
    Task CreateAccount(int accountNumber, string accountHolder, double initialDeposit = 0);
    Task Deposit(int accountNumber, double amount);
    Task Withdraw(int accountNumber, double amount);
    Task<BankAccount> GetAccountDetails(int accountNumber);
}
