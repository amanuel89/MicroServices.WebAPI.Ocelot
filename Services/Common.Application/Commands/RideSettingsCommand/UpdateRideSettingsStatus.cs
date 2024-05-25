namespace RideBackend.Application.Commands;

public class UpdateRideSettingsStatus : IRequest<OperationResult<RideSettings>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdateRideSettingsStatusHandler : IRequestHandler<UpdateRideSettingsStatus, OperationResult<RideSettings>>
{
    private readonly IRepositoryBase<RideSettings> _RideSettings;
    public UpdateRideSettingsStatusHandler(IRepositoryBase<RideSettings> _RideSettings) => this._RideSettings = _RideSettings;
    public async Task<OperationResult<RideSettings>> Handle(UpdateRideSettingsStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<RideSettings>();
        var RideSettings = _RideSettings.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (RideSettings == null)
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record doesn't exist.");
            return result;
        }
        RideSettings.UpdateRecordStatus(request.RecordStatus);
        _RideSettings.Update(RideSettings);

        result.Message = "Operation success";
        return result;
    }
}
