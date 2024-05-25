namespace RideBackend.Application.Commands;

public class DeletePassenger : IRequest<OperationResult<Passenger>>
{
    public long Id { get; set; }
}
internal class DeletePassengerHandler : IRequestHandler<DeletePassenger, OperationResult<Passenger>>
{

    private readonly IRepositoryBase<Passenger> _Passenger;
    public DeletePassengerHandler(IRepositoryBase<Passenger> _Passenger) => this._Passenger = _Passenger;
    public async Task<OperationResult<Passenger>> Handle(DeletePassenger request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Passenger>();

        var Passenger = _Passenger.FirstOrDefault(x => x.Id == request.Id);

        if (Passenger == null || Passenger.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        if (Passenger.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
   
        Passenger.Delete();
        _Passenger.Update(Passenger);

        result.Payload = Passenger;
        result.Message = "Operation success";
        return result;
    }
}