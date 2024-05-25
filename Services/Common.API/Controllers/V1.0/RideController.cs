using RideBackend.Application.Commands.PassengerCommand;
using RideBackend.Domain.Dtos;

namespace RideBackend.API.Controllers.V1._0;
public class RideController : BaseController
{
    [HttpPost("Create")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromQuery] CreateRideRequestDto request)
    {
        var result = await _mediator.Send(new CreateRide { Ride = request });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPost("CreateByDriver")]
    [ValidateModel]
    public async Task<IActionResult> CreateByDriver([FromQuery] CreateRideByDriverRequestDto request)
    {
        var result = await _mediator.Send(new CreateRide { RideByDriver = request,BookedByDriver=true });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("Accept")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromQuery] AcceptRideRequestDto request)
    {
        var result = await _mediator.Send(new AcceptRide { DriverId =request.DriverId, OrderId = request.OrderId });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("Complete")]
    [ValidateModel]
    public async Task<IActionResult> Complete([FromQuery] CompleteRideRequestDto request)
    {
        var result = await _mediator.Send(new CompleteRide { OrderId = request.OrderId ,DistanceDriven=request.DistanceDriven,DriverId=request.DriverId  });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }

    [HttpPut("StartRide")]
    [ValidateModel]
    public async Task<IActionResult> Cancel([FromQuery] StartRideRequestDto request)
    {
        var result = await _mediator.Send(new StartRide { OrderId = request.OrderId });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    }


}

