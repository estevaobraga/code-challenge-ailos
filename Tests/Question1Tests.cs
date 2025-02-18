using System.Threading.Tasks;
using Moq;
using Questao1.Models;
using Questao1.Repositories.Interfaces;
using Questao1.Services;

namespace Tests;

public class Question1Tests
{
    private readonly Mock<IAccountRepository> _mockRepo;
    private readonly AccountService _accountService;

    public Question1Tests()
    {
        _mockRepo = new Mock<IAccountRepository>();
        _accountService = new AccountService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAccount_WithInitialDeposit_ShouldSetBalanceCorrectly()
    {
        // Arrange
        int accountNumber = 1234;
        string accountHolder = "John Doe";
        double initialDeposit = 100.00;

        // Act
        await _accountService.CreateAccount(accountNumber, accountHolder, initialDeposit);

        // Assert
        _mockRepo.Verify(repo => repo.AddAccount(It.Is<BankAccount>(account =>
            account.AccountNumber == accountNumber &&
            account.HolderName == accountHolder &&
            account.Balance == initialDeposit
        )), Times.Once);
    }

    [Fact]
    public async Task CreateAccount_WithoutInitialDeposit_ShouldSetBalanceToZero()
    {
        // Arrange
        int accountNumber = 1234;
        string accountHolder = "John Doe";

        // Act
        await _accountService.CreateAccount(accountNumber, accountHolder);

        // Assert
        _mockRepo.Verify(repo => repo.AddAccount(It.Is<BankAccount>(account =>
            account.AccountNumber == accountNumber &&
            account.HolderName == accountHolder &&
            account.Balance == 0
        )), Times.Once);
    }

    [Fact]
    public async Task Deposit_ValidAmount_ShouldIncreaseBalance()
    {
        // Arrange
        int accountNumber = 1234;
        string HolderName = "John Doe";
        double initialBalance = 100.00;
        double depositAmount = 50.00;

        var account = new BankAccount(accountNumber, HolderName, initialBalance);
        _mockRepo.Setup(repo => repo.GetAccount(accountNumber)).ReturnsAsync(account);

        // Act
        await _accountService.Deposit(accountNumber, depositAmount);

        // Assert
        Assert.Equal(initialBalance + depositAmount, account.Balance);
        _mockRepo.Verify(repo => repo.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public async Task Deposit_InvalidAmount_ShouldThrowException()
    {
        // Arrange
        int accountNumber = 1234;
        double invalidDepositAmount = -50.00;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _accountService.Deposit(accountNumber, invalidDepositAmount));

        Assert.Equal("Deposit amount must be greater than zero.", exception.Message);
    }

    [Fact]
    public async Task Withdraw_ValidAmount_ShouldDecreaseBalanceIncludingFee()
    {
        // Arrange
        int accountNumber = 1234;
        string accountHolder = "John Doe";
        double initialBalance = 100.00;
        double withdrawalAmount = 50.00;

        var account = new BankAccount(accountNumber, accountHolder, initialBalance);
        _mockRepo.Setup(repo => repo.GetAccount(accountNumber)).ReturnsAsync(account);

        // Act
        await _accountService.Withdraw(accountNumber, withdrawalAmount);

        // Assert
        Assert.Equal(initialBalance - withdrawalAmount - AccountService.WithdrawalFee, account.Balance);
        _mockRepo.Verify(repo => repo.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public async Task Withdraw_InvalidAmount_ShouldThrowException()
    {
        // Arrange
        int accountNumber = 1234;
        double invalidWithdrawalAmount = -50.00;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _accountService.Withdraw(accountNumber, invalidWithdrawalAmount));

        Assert.Equal("Withdrawal amount must be greater than zero.", exception.Message);
    }

    [Fact]
    public async Task Withdraw_MoreThanBalance_ShouldAllowNegativeBalance()
    {
        // Arrange
        int accountNumber = 1234;
        string accountHolder = "John Doe";
        double initialBalance = 50.00;
        double withdrawalAmount = 60.00;

        var account = new BankAccount(accountNumber, accountHolder, initialBalance);
        _mockRepo.Setup(repo => repo.GetAccount(accountNumber)).ReturnsAsync(account);

        // Act
        await _accountService.Withdraw(accountNumber, withdrawalAmount);

        // Assert
        Assert.Equal(initialBalance - withdrawalAmount - AccountService.WithdrawalFee, account.Balance);
        Assert.True(account.Balance < 0); // Balance should be negative
        _mockRepo.Verify(repo => repo.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public async Task GetAccountDetails_ShouldReturnCorrectAccount()
    {
        // Arrange
        int accountNumber = 1234;
        string accountHolder = "John Doe";
        double initialBalance = 100.00;

        var account = new BankAccount(accountNumber, accountHolder, initialBalance);
        _mockRepo.Setup(repo => repo.GetAccount(accountNumber)).ReturnsAsync(account);

        // Act
        var result = await _accountService.GetAccountDetails(accountNumber);

        // Assert
        Assert.Equal(account, result);
    }
}