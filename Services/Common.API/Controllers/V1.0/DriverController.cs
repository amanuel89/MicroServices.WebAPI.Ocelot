using IdentityServer.Application.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace RideBackend.API.Controllers.V1._0;
public class DriverController : BaseController
{
    private readonly IHubContext<RideHub> _hubContext;
    private readonly IRedisHelper _redisHelper;
    public DriverController(IHubContext<RideHub> hubContext, IRedisHelper redisHelper)
    {
        _hubContext = hubContext;
        _redisHelper = redisHelper;
    }


    [HttpPost("Create")]
    [RequestSizeLimit(3_000_000_000)]
    [ValidateModel]
    public async Task<IActionResult> Create([FromForm] DriverCreateRequestDTO request)
    {
        var result = await _mediator.Send(new CreateDriver {Driver=request});
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromForm] DriverUpdateRequest request, long id)
    {
        var result = await _mediator.Send(new UpdateDriver { Driver = request, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("ChangeVehicle/{id}")]
    [ValidateModel]
    public async Task<IActionResult> ChangeVehicle([FromForm] VehicleCreateRequest request, long id)
    {
        var result = await _mediator.Send(new ChangeVehicle { Vehicle = request, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateDriverStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteDriver { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetDriverList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetDriver { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("GetByUsername/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _mediator.Send(new GetDriverByUserName { Username = username });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpGet("GetDriverVehicleHistory")]
    public async Task<IActionResult> GetDriverVehicleHistory(long driverId,RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetDriverVehicleHistory {DriverId=driverId, RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

   // DriverLocationUpdateRequest request
    [HttpPost("UpdateDriverLocation")]
    public async Task<IActionResult> UpdateDriverLocation(DriverLocationUpdateRequest request)
    {
        string serializedRequest = JsonConvert.SerializeObject(request);
        var location = _redisHelper.WriteToCache("DRIVER-LOCATION_"+request.DriverId, serializedRequest);
        await _hubContext.Clients.All.SendAsync("newLocation", request);
        return Ok();
    }
}
