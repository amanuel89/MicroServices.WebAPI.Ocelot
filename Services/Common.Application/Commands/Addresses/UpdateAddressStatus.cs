namespace RideBackend.Application.Commands;

public class UpdateAddressStatus : IRequest<OperationResult<Address>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdateAddressStatusHandler : IRequestHandler<UpdateAddressStatus, OperationResult<Address>>
{
    private readonly IRepositoryBase<Address> _Address;
    public UpdateAddressStatusHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
    public async Task<OperationResult<Address>> Handle(UpdateAddressStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Address>();
        var Address = _Address.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (Address == null)
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record doesn't exist.");
            return result;
        }
        Address.UpdateRecordStatus(request.RecordStatus);
        _Address.Update(Address);

        result.Payload = Address;
        result.Message = "Operation success";
        return result;
    }
}
