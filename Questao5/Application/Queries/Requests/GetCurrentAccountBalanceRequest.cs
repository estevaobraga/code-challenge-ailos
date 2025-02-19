using System;
using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

public class GetCurrentAccountBalanceRequest : IRequest<GetCurrentAccountBalanceResponse>
{
    public string? CurrentAccountId { get; set; }
}
