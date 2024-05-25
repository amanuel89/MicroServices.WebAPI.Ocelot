namespace RideBackend.Application.Commands;

public class DeleteVehicleTypes : IRequest<OperationResult<VehicleTypes>>
{
    public long Id { get; set; }
}
internal class DeleteVehicleTypesHandler : IRequestHandler<DeleteVehicleTypes, OperationResult<VehicleTypes>>
{

    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    public DeleteVehicleTypesHandler(IRepositoryBase<VehicleTypes> _VehicleTypes) => this._VehicleTypes = _VehicleTypes;
    public async Task<OperationResult<VehicleTypes>> Handle(DeleteVehicleTypes request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<VehicleTypes>();

        var VehicleTypes = _VehicleTypes.FirstOrDefault(x => x.Id == request.Id);
        if (VehicleTypes == null || VehicleTypes.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (VehicleTypes.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
        VehicleTypes.Delete();
        _VehicleTypes.Update(VehicleTypes);
        result.Message = "Operation success";
        return result;
    }
}