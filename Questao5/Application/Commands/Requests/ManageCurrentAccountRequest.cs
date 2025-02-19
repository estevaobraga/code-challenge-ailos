using System;
using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests;

public class ManageCurrentAccountRequest : IRequest<ManageCurrentAccountResponse>
{
    public string? Id { get; set; }
    public string? CurrentAccountId { get; set; }
    public decimal Value { get; set; }
    public MovimentType MovimentType { get; set; }
}
