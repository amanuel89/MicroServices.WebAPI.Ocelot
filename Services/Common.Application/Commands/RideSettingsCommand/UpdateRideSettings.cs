using AutoMapper;
using RideBackend.Domain.Dtos;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;

public class UpdateRideSettings : IRequest<OperationResult<SettingsResponseDto>>
{
    public long Id { get; set; }
    public SettingRequestUpdateDto RideSettingsUpdateRequesDto { get; set; }
}
public class UpdateRideSettingsHandler : IRequestHandler<UpdateRideSettings, OperationResult<SettingsResponseDto>>
{
    private readonly IRepositoryBase<RideSettings> _RideSettings;
    private readonly IMapper _mapper;
    public UpdateRideSettingsHandler(IRepositoryBase<VehicleTypes> _VehicleTypes, IMapper mapper, IRepositoryBase<RideSettings> RideSettings)
    {

        _mapper = mapper;
        _RideSettings = RideSettings;
    }
    public async Task<OperationResult<SettingsResponseDto>> Handle(UpdateRideSettings request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SettingsResponseDto>();
        var RideSettings = _RideSettings.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (RideSettings == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        var req = request.RideSettingsUpdateRequesDto;
        RideSettings.Update(req.MaximumDriverDistanceKm, req.DriverLocationUpdateIntervalMs, req.DriverInactivityTimeoutMs, req.ScheduledRides, req.CallCenterNumber, req.MaximumPendingBookings, req.MinimumBookingIntervalMn, req.DriverDefaultCommission, req.DriverMinimumWalletBalance, req.DriverMinimumWithdrawalBalanceAmount);
        _RideSettings.Update(RideSettings);

        var response = _mapper.Map<SettingsResponseDto>(RideSettings);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}
