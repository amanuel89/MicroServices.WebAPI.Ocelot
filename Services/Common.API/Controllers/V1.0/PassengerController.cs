using RideBackend.Application.Commands.PassengerCommand;

namespace RideBackend.API.Controllers.V1._0;
public class PassengerController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromForm] PassengerRequestDTO request)
    {
        var result = await _mediator.Send(new CreatePassenger {Passenger=request});
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("Update/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromForm] PassengerUpdateDTO request, long id)
    {
        var result = await _mediator.Send(new UpdatePassenger {Passenger=request, Id = id });
         return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("UpdateStatus/{id}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromBody] RecordStatusDto request, long id)
    {
        var result = await _mediator.Send(new UpdatePassengerStatus { RecordStatus = request.Status, Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpPut("VerifyPhone")]
    [ValidateModel]
    public async Task<IActionResult> VerifyPhone(long verifierId,string phoneNumber,string verificationCode)
    {
        var result = await _mediator.Send(new VerifyPassengerPhone {
        Id = verifierId,
        Code = verificationCode,
        Phone=phoneNumber
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeletePassenger { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(RecordStatus? recordStatus, int pageSize=0, int pageNumber = 0)
    {

        var result = await _mediator.Send(new GetPassengerList { RecordStatus = recordStatus, PageNumber = pageNumber, PageSize = pageSize });
         return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetPassenger { Id = id });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

}
