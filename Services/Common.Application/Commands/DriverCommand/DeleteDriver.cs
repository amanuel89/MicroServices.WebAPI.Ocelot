namespace RideBackend.Application.Commands;

public class DeleteDriver : IRequest<OperationResult<Driver>>
{
    public long Id { get; set; }
}
internal class DeleteDriverHandler : IRequestHandler<DeleteDriver, OperationResult<Driver>>
{

    private readonly IRepositoryBase<Driver> _Driver;
    public DeleteDriverHandler(IRepositoryBase<Driver> _Driver) => this._Driver = _Driver;
    public async Task<OperationResult<Driver>> Handle(DeleteDriver request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Driver>();

        var Driver = _Driver.FirstOrDefault(x => x.Id == request.Id);

        if (Driver == null || Driver.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        if (Driver.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted. Record Is Read Only");
            return result;
        }
   
        Driver.Delete();
        _Driver.Update(Driver);
        result.Payload = Driver;
        result.Message = "Operation success";
        return result;
    }
}