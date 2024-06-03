using Common.Application.Commands.CountryCommand;

namespace CommonService.API.Controllers.V1._0;
public class CountryController : BaseController
{
    
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _mediator.Send(new UpdateCountryStatus { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
  

}
