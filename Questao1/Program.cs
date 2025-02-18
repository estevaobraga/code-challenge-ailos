using System;
using System.Globalization;
using System.Threading.Tasks;
using Questao1.Repositories;
using Questao1.Repositories.Interfaces;
using Questao1.Services;
using Questao1.Services.Interfaces;

namespace Questao1 {
    class Program {

        static async Task Main(string[] args) {
            IAccountRepository accountRepository = new AccountRepository();
            IAccountService accountService = new AccountService(accountRepository);

            Console.Write("Entre o número da conta: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Entre o titular da conta: ");
            string titular = Console.ReadLine();
            Console.Write("Haverá depósito inicial (s/n)? ");
            char resp = char.Parse(Console.ReadLine());
            if (resp == 's' || resp == 'S') {
                Console.Write("Entre o valor de depósito inicial: ");
                double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                //conta = new BankAccount(numero, titular, depositoInicial);
                await accountService.CreateAccount(numero, titular, depositoInicial);
            }
            else {
                //conta = new BankAccount(numero, titular);
                await accountService.CreateAccount(numero, titular);
            }

            Console.WriteLine();
            Console.WriteLine("Dados da conta:");
            await DisplayAccountDetails(numero, accountService);

            Console.WriteLine();
            Console.Write("Entre um valor para depósito: ");
            double quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            await accountService.Deposit(numero, quantia);
            Console.WriteLine("Dados da conta atualizados:");
            await DisplayAccountDetails(numero, accountService);

            Console.WriteLine();
            Console.Write("Entre um valor para saque: ");
            quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            await accountService.Withdraw(numero, quantia);
            Console.WriteLine("Dados da conta atualizados:");
            await DisplayAccountDetails(numero, accountService);

            /* Output expected:
            Exemplo 1:

            Entre o número da conta: 5447
            Entre o titular da conta: Milton Gonçalves
            Haverá depósito inicial(s / n) ? s
            Entre o valor de depósito inicial: 350.00

            Dados da conta:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 350.00

            Entre um valor para depósito: 200
            Dados da conta atualizados:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 550.00

            Entre um valor para saque: 199
            Dados da conta atualizados:
            Conta 5447, Titular: Milton Gonçalves, Saldo: $ 347.50

            Exemplo 2:
            Entre o número da conta: 5139
            Entre o titular da conta: Elza Soares
            Haverá depósito inicial(s / n) ? n

            Dados da conta:
            Conta 5139, Titular: Elza Soares, Saldo: $ 0.00

            Entre um valor para depósito: 300.00
            Dados da conta atualizados:
            Conta 5139, Titular: Elza Soares, Saldo: $ 300.00

            Entre um valor para saque: 298.00
            Dados da conta atualizados:
            Conta 5139, Titular: Elza Soares, Saldo: $ -1.50
            */
        }

        private static async Task DisplayAccountDetails(int accountNumber, IAccountService accountService)
        {
            var account = await accountService.GetAccountDetails(accountNumber);
            Console.WriteLine($"Conta {account.AccountNumber}, Titular: {account.HolderName}, Saldo: $ {account.Balance:F2}");
        }
    }
}
