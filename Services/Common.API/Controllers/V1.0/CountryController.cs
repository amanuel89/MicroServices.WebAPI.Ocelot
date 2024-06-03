﻿using Common.Application.Commands.CountryCommand;
using CommonService.Application.Commands;

namespace CommonService.API.Controllers.V1._0;
public class CountryController : BaseController
{
    #region Command
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CountryCreateRequest request)
    {
        var result = await _mediator.Send(new CreateCountry { CountryRequest = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] CountryUpdateRequest request)
    {
        var result = await _mediator.Send(new UpdateCountry { CountryUpdateRequest = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _mediator.Send(new UpdateCountryStatus { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteCountry { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    #endregion


    #region Query

    #endregion
}
