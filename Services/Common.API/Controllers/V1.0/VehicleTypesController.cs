namespace RideBackend.API.Controllers.V1._0;
public class VehicleTypesController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromForm] VehicleTypesCreateDto request)
    {
        var result = await _mediator.Send(new CreateVehicleTypes {VehicleType=request});
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromForm] VehicleTypesUpdateDto request, long id)
    {
        var result = await _mediator.Send(new UpdateVehicleTypes {VehicleTypes=request, Id = id });
         return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdateVehicleTypesStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteVehicleTypes { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize = 0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetVehicleTypesList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetVehicleTypes { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

}
