using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrentAccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrentAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Manage balance account with moviments type credit and debit
    /// </summary>
    /// <param name="ManageCurrentAccountRequest">request</param>
    /// <returns>The account moviment</returns>
    /// <response code="201">Returns the created account moviment</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost("manage")]
    public async Task<IActionResult> ManageCurrentAccount([FromBody] ManageCurrentAccountRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message, type = ex.Message });
        }
    }
    
    /// <summary>
    /// Get current account balance
    /// </summary>
    /// <param name="GetCurrentAccountBalanceRequest">request</param>
    /// <returns>The account balance result</returns>
    /// <response code="201">Returns the account balance informations</response>
    /// <response code="400">If the request is invalid</response>
    [HttpGet("balance")]
    public async Task<IActionResult> GetCurrentAccountBalance([FromQuery] GetCurrentAccountBalanceRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message, type = ex.Message });
        }
    }
}
