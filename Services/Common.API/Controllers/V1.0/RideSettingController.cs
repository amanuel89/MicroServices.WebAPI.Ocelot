
using RideBackend.Application.Commands.PassengerCommand;
using RideBackend.Domain.Dtos;

namespace RideBackend.API.Controllers.V1._0;
public class RideSettingController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] SettingRequestDto request)
    {
        var result = await _mediator.Send(new UpSertRideSettings { RideSettingsRequesDto = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPost("UpSertRideSettings")]
    [ValidateModel]
    public async Task<IActionResult> UpSertRideSettings([FromBody] SettingRequestDto request)
    {
        var result = await _mediator.Send(new UpSertRideSettings { RideSettingsRequesDto = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] SettingRequestUpdateDto request, long id)
    {
        var result = await _mediator.Send(new UpdateRideSettings { RideSettingsUpdateRequesDto = request, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateRideSettingsStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteRideSetting { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetRideSettingsList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetRideSettings { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

}
