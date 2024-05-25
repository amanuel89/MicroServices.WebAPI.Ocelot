using AutoMapper;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class AcceptRide : IRequest<OperationResult<RideResponseDto>>
{
    public long OrderId { get; set; }
    public long DriverId { get; set; }
}
public class AcceptRideHandler : IRequestHandler<AcceptRide, OperationResult<RideResponseDto>>
{
    private readonly IRepositoryBase<Ride> _Rides;
    private readonly IRepositoryBase<RideSettings> _RideSettings;
    private readonly IMapper _mapper;
    public AcceptRideHandler(IRepositoryBase<Ride> _Rides, IMapper imapper, IRepositoryBase<RideSettings> rideSettings)
    {
        this._Rides = _Rides;
        this._mapper = imapper;
        _RideSettings = rideSettings;
    }
    public async Task<OperationResult<RideResponseDto>> Handle(AcceptRide request, CancellationToken cancellationToken)
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
        Ride.Accepted(request.DriverId);
        _Rides.Update(Ride);

        var response = _mapper.Map<RideResponseDto>(Ride);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
