namespace RideBackend.Application.Commands;

public class UpdatePassengerStatus : IRequest<OperationResult<Passenger>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdatePassengerStatusHandler : IRequestHandler<UpdatePassengerStatus, OperationResult<Passenger>>
{
    private readonly IRepositoryBase<Passenger> _Passenger;
    public UpdatePassengerStatusHandler(IRepositoryBase<Passenger> _Passenger) => this._Passenger = _Passenger;
    public async Task<OperationResult<Passenger>> Handle(UpdatePassengerStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Passenger>();
        var Passenger = _Passenger.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (Passenger == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        Passenger.UpdateRecordStatus(request.RecordStatus);
        _Passenger.Update(Passenger);

        result.Payload = Passenger;
        result.Message = "Operation success";
        return result;
    }
}
