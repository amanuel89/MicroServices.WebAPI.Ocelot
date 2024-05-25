using AutoMapper;
using RideBackend.Domain.Dtos;
using RideBackend.Domain.Models;

namespace RideBackend.Application.Commands;
public class UpSertRideSettings : IRequest<OperationResult<SettingsResponseDto>>
{
    public SettingRequestDto RideSettingsRequesDto { get; set; }
}
public class CreateRideSettingsHandler : IRequestHandler<UpSertRideSettings, OperationResult<SettingsResponseDto>>
{
    private readonly IRepositoryBase<RideSettings> _RideSettings;
    private readonly IMapper _mapper;
    public CreateRideSettingsHandler(IMapper mapper, IRepositoryBase<RideSettings> RideSettings)
    {
        _mapper = mapper;
        _RideSettings = RideSettings;
    }
    public async Task<OperationResult<SettingsResponseDto>> Handle(UpSertRideSettings request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SettingsResponseDto>();

        if (request.RideSettingsRequesDto==null)
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Request cannot be empty.");
            return result;
        }

        var req = request.RideSettingsRequesDto;
        var setting = _RideSettings.FirstOrDefault(x => x.RecordStatus == RecordStatus.Active);
        if (setting == null)
        {
            var RideSetting = RideSettings.Create(req.MaximumDriverDistanceKm, req.DriverLocationUpdateIntervalMs, req.DriverInactivityTimeoutMs, req.ScheduledRides, req.CallCenterNumber, req.MaximumPendingBookings, req.MinimumBookingIntervalMn, req.DriverDefaultCommission, req.DriverMinimumWalletBalance, req.DriverMinimumWithdrawalBalanceAmount);
            await _RideSettings.AddAsync(RideSetting);
            var response = _mapper.Map<SettingsResponseDto>(RideSetting);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
        else
        {
            setting.Update(req.MaximumDriverDistanceKm, req.DriverLocationUpdateIntervalMs, req.DriverInactivityTimeoutMs, req.ScheduledRides, req.CallCenterNumber, req.MaximumPendingBookings, req.MinimumBookingIntervalMn, req.DriverDefaultCommission, req.DriverMinimumWalletBalance, req.DriverMinimumWithdrawalBalanceAmount);
            _RideSettings.Update(setting);
            var response = _mapper.Map<SettingsResponseDto>(setting);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
