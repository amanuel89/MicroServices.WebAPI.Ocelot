namespace RideBackend.Application.Commands;

public class DeleteAddress : IRequest<OperationResult<Address>>
{
    public long Id { get; set; }
}
internal class DeleteAddressHandler : IRequestHandler<DeleteAddress, OperationResult<Address>>
{

    private readonly IRepositoryBase<Address> _Address;
    public DeleteAddressHandler(IRepositoryBase<Address> _Address) => this._Address = _Address;
    public async Task<OperationResult<Address>> Handle(DeleteAddress request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Address>();

        var Address = _Address.FirstOrDefault(x => x.Id == request.Id);
        var IsParent = _Address.FirstOrDefault(x => x.ParentID == request.Id);
        if (Address == null || Address.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (Address.IsReadOnly)
        {
            result.AddError(ErrorCode.ServerError, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
        if (IsParent != null)
        {
            result.AddError(ErrorCode.ServerError, "Record cannot be deleted. Record is Parent to Another Category");
            return result;
        }
        Address.Delete();
        _Address.Update(Address);

        result.Payload = Address;
        result.Message = "Operation success";
        return result;
    }
}