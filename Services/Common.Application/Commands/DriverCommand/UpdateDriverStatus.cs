namespace RideBackend.Application.Commands;

public class UpdateDriverStatus : IRequest<OperationResult<Driver>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdateDriverStatusHandler : IRequestHandler<UpdateDriverStatus, OperationResult<Driver>>
{
    private readonly IRepositoryBase<Driver> _Driver;
    public UpdateDriverStatusHandler(IRepositoryBase<Driver> _Driver) => this._Driver = _Driver;
    public async Task<OperationResult<Driver>> Handle(UpdateDriverStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Driver>();
        var Driver = _Driver.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (Driver == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        Driver.UpdateRecordStatus(request.RecordStatus);
        _Driver.Update(Driver);

        result.Payload = Driver;
        result.Message = "Operation success";
        return result;
    }
}
