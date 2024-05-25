namespace RideBackend.Application.Commands;

public class UpdateAddress : IRequest<OperationResult<Address>>
{
    public long Id { get; set; }
    public string AddressName { get; set; }
    public long? ParentID { get; set; }
    public AddressType AddressType { get; set; }
}
public class EditAddressHandler : IRequestHandler<UpdateAddress, OperationResult<Address>>
{
    private readonly IRepositoryBase<Address> _Address;
    public EditAddressHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
    public async Task<OperationResult<Address>> Handle(UpdateAddress request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Address>();
        var Address = _Address.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);

        if (request.Id == request.ParentID)
        {
            result.AddError(ErrorCode.NotFound, "Address cannot be parent to itself");
            return result;
        }


        if (Address == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (_Address.ExistWhere(x => x.AddressName == request.AddressName && x.Id!=request.Id))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record already exists.");
            return result;
        }
        Address.Update(request.AddressName, request.ParentID == 0 ? null : request.ParentID, request.AddressType);
        _Address.Update(Address);

        result.Payload = Address;
        result.Message = "Operation success";
        return result;
    }
}
