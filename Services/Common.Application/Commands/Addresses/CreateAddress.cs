namespace RideBackend.Application.Commands;
public class CreateAddress : IRequest<OperationResult<Address>>
{
    public string AddressName { get;  set; }
    public long? ParentID { get;  set; } 
    public AddressType AddressType { get;  set; }
}
public class CreateAddressHandler : IRequestHandler<CreateAddress, OperationResult<Address>>
{
    private readonly IRepositoryBase<Address> _Address;
    public CreateAddressHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
    public async Task<OperationResult<Address>> Handle(CreateAddress request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Address>();

        if (_Address.ExistWhere(x => (x.AddressName == request.AddressName) && x.RecordStatus != RecordStatus.Deleted))
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record already exists.");
            return result;
        }
        var address = Address.Create(request.AddressName,request.ParentID==0?null:request.ParentID,request.AddressType);
        await _Address.AddAsync(address);
        result.Payload = address;
        result.Message = "Operation success";
        return result;
    }
}
