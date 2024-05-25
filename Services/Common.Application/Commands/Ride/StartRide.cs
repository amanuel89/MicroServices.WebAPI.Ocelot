using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class StartRide : IRequest<OperationResult<RideResponseDto>>
{
    public long OrderId { get; set; }
}
public class StartRideHandler : IRequestHandler<StartRide, OperationResult<RideResponseDto>>
{
    private readonly IRepositoryBase<Ride> _Rides;
    private readonly IRepositoryBase<Ride> _RideSettings;
    private readonly IMapper _mapper;
    public StartRideHandler(IRepositoryBase<Ride> _Rides, IMapper imapper, IRepositoryBase<Ride> rideSettings)
    {
        this._Rides = _Rides;
        this._mapper = imapper;
        _RideSettings = rideSettings;
    }
    public async Task<OperationResult<RideResponseDto>> Handle(StartRide request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<RideResponseDto>();
        var RideSettings = _RideSettings.FirstOrDefault(x => x.RecordStatus == RecordStatus.Active);
        if (RideSettings == null)
        {
            result.AddError(ErrorCode.NotFound, "Unable to find active settings.");
            return result;
        }
        var Ride = _Rides.FirstOrDefault(x => x.Id == request.OrderId && x.RecordStatus != RecordStatus.Deleted);
        if (Ride == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        Ride.RideStarted();
        _Rides.Update(Ride);

        var response = _mapper.Map<RideResponseDto>(Ride);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
