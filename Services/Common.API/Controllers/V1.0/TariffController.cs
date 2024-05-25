
using RideBackend.Application.Commands.PassengerCommand;
using RideBackend.Domain.Dtos;

namespace RideBackend.API.Controllers.V1._0;
public class TariffController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] TariffRequesDto request)
    {
        var result = await _mediator.Send(new CreateTariff { TariffRequesDto = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] TariffUpdateRequesDto request, long id)
    {
        var result = await _mediator.Send(new UpdateTariff { TariffUpdateRequesDto = request, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateTariffStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteTariff { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetTarifList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetTariff { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

}
