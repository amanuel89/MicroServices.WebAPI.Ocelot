namespace RideBackend.Application.Commands;

public class DeleteRideSetting : IRequest<OperationResult<RideSettings>>
{
    public long Id { get; set; }
}
internal class DeleteRideSettingsHandler : IRequestHandler<DeleteRideSetting, OperationResult<RideSettings>>
{

    private readonly IRepositoryBase<RideSettings> _RideSettings;
    public DeleteRideSettingsHandler(IRepositoryBase<RideSettings> _RideSettings) => this._RideSettings = _RideSettings;
    public async Task<OperationResult<RideSettings>> Handle(DeleteRideSetting request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<RideSettings>();

        var RideSettings = _RideSettings.FirstOrDefault(x => x.Id == request.Id);
        if (RideSettings == null || RideSettings.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (RideSettings.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
        RideSettings.Delete();
        _RideSettings.Update(RideSettings);
        result.Message = "Operation success";
        return result;
    }
}