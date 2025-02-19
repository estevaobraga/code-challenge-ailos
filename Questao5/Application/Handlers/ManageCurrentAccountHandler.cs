using System;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Handlers;

public class ManageCurrentAccountHandler : IRequestHandler<ManageCurrentAccountRequest, ManageCurrentAccountResponse>
{
    private readonly ICommandStore _commandStore;
    private readonly IQueryStore _queryStore;
    // TODO : Needs refact: Implement IValidator from FluentValidator to uncouple the validation codes

    public ManageCurrentAccountHandler(ICommandStore commandStore, IQueryStore queryStore)
    {
        _commandStore = commandStore;
        _queryStore = queryStore;
    }

    // TODO : Needs refact: Implement IValidator from FluentValidator to uncouple the validation codes
    public async Task<ManageCurrentAccountResponse> Handle(ManageCurrentAccountRequest request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.CurrentAccountId))
            throw new Exception("INVALID_CURRENT_ACCOUNT_ID");

        if (string.IsNullOrEmpty(request.Id))
            throw new Exception("INVALID_MOVIMENT_ID");

        var currentAccount = await _queryStore.GetCurrentAccountById(request.CurrentAccountId);

        if (currentAccount == null)
            throw new Exception("INVALID_ACCOUNT");

        if (!currentAccount.Active)
            throw new Exception("INACTIVE_ACCOUNT");

        if (request.Value <= 0)
            throw new Exception("INVALID_VALUE");

        if (request.MovimentType != MovimentType.Credit && request.MovimentType != MovimentType.Debit)
            throw new Exception("INVALID_TYPE");

        var accountMoviment = new AccountMoviment
        {
            Id = request.Id,
            CurrentAccountId = currentAccount.Id,
            Date = DateTime.Now.ToString("dd/MM/yyyy"),
            MovimentType = ((char)request.MovimentType).ToString(),
            Value = request.Value
        };

        await _commandStore.AddMoviment(accountMoviment);

        return new ManageCurrentAccountResponse { MovimentId = accountMoviment.Id };
    }
}
