using System;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore;

public class QueryStore : IQueryStore
{
    private readonly string _connectionString;

    public QueryStore(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<CurrentAccount> GetCurrentAccountById(string currentAccountId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<CurrentAccount>(
                "SELECT idcontacorrente Id, numero AccountNumber, nome HolderName, ativo Active FROM contacorrente WHERE idcontacorrente = @currentAccountId",
                new { currentAccountId });
        }
    }

    public async Task<decimal> GetCurrentAccountBalance(string currentAccountId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            var credits = await connection.QueryFirstOrDefaultAsync<decimal?>(
                "SELECT SUM(valor) FROM movimento WHERE idcontacorrente = @currentAccountId AND tipomovimento = 'C'",
                new { currentAccountId }) ?? 0;

            var debits = await connection.QueryFirstOrDefaultAsync<decimal?>(
                "SELECT SUM(valor) FROM movimento WHERE idcontacorrente = @currentAccountId AND tipomovimento = 'D'",
                new { currentAccountId }) ?? 0;

            return (credits - debits);
        }
    }
}
