using System;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore;

public class CommandStore : ICommandStore
{
    private readonly string _connectionString;

    public CommandStore(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddMoviment(AccountMoviment moviment)
    {
        try
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @CurrentAccountId, @Date, @MovimentType, @Value)",
                    moviment);
            }
        } 
        // TODO : tightly couples, this violates the DIP SOLID principle, needs refact
        catch (SqliteException sqliteException){
            if(sqliteException.ErrorCode == 19){
                throw new Exception("Moviment id already processed");
            }
            throw sqliteException;
        }
    }
}
