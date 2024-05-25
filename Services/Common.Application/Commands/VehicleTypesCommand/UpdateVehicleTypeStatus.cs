namespace RideBackend.Application.Commands;

public class UpdateVehicleTypesStatus : IRequest<OperationResult<VehicleTypes>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdateVehicleTypesStatusHandler : IRequestHandler<UpdateVehicleTypesStatus, OperationResult<VehicleTypes>>
{
    private readonly IRepositoryBase<VehicleTypes> _VehicleTypes;
    public UpdateVehicleTypesStatusHandler(IRepositoryBase<VehicleTypes> _VehicleTypes) => this._VehicleTypes = _VehicleTypes;
    public async Task<OperationResult<VehicleTypes>> Handle(UpdateVehicleTypesStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<VehicleTypes>();
        var VehicleTypes = _VehicleTypes.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (VehicleTypes == null)
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record doesn't exist.");
            return result;
        }
        VehicleTypes.UpdateRecordStatus(request.RecordStatus);
        _VehicleTypes.Update(VehicleTypes);

        result.Message = "Operation success";
        return result;
    }
}
