﻿using Common.Application.Commands.CurrencyCommand;
using CommonService.Application.Commands;

namespace CommonService.API.Controllers.V1._0;
public class CurrencyController : BaseController
{
    #region Command
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CurrencyCreateRequest request)
    {
        var result = await _mediator.Send(new CurrencyCreateCommand { CurrencyRequest = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] CurrencyUpdateRequest request)
    {
        var result = await _mediator.Send(new UpdateCurrency { CurrencyUpdateRequest = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _mediator.Send(new UpdateSystemLookUpStatus { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteSystemLookUp { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    #endregion


}