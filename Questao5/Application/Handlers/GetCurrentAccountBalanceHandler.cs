using System;
using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Handlers;

public class GetCurrentAccountBalanceHandler: IRequestHandler<GetCurrentAccountBalanceRequest, GetCurrentAccountBalanceResponse>
{
    private readonly IQueryStore _queryStore;
    // TODO : Needs refact: Implement IValidator from FluentValidator to uncouple the validation codes

    public GetCurrentAccountBalanceHandler(IQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<GetCurrentAccountBalanceResponse> Handle(GetCurrentAccountBalanceRequest request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.CurrentAccountId))
            throw new Exception("INVALID_CURRENT_ACCOUNT_ID");

        var currentAccount = await _queryStore.GetCurrentAccountById(request.CurrentAccountId);

        if (currentAccount == null)
            throw new Exception("INVALID_ACCOUNT");

        if (!currentAccount.Active)
            throw new Exception("INACTIVE_ACCOUNT");

        var balance = await _queryStore.GetCurrentAccountBalance(request.CurrentAccountId);

        return new GetCurrentAccountBalanceResponse
        {
            CurrentAccountNumber = currentAccount.AccountNumber,
            HolderName = currentAccount.HolderName ?? "",
            QueryDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            Balance = balance
        };
    }
}
