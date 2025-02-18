using System.Threading.Tasks;
using Questao1.Models;

namespace Questao1.Repositories.Interfaces;

public interface IAccountRepository
{
    Task AddAccount(BankAccount account);
    Task<BankAccount> GetAccount(int accountNumber);
    Task UpdateAccount(BankAccount account);
}
