namespace RideBackend.Application.Commands;

public class UpdateTariffStatus : IRequest<OperationResult<Tariff>>
{
    public long Id { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
public class UpdateTariffStatusHandler : IRequestHandler<UpdateTariffStatus, OperationResult<Tariff>>
{
    private readonly IRepositoryBase<Tariff> _Tariff;
    public UpdateTariffStatusHandler(IRepositoryBase<Tariff> _Tariff) => this._Tariff = _Tariff;
    public async Task<OperationResult<Tariff>> Handle(UpdateTariffStatus request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Tariff>();
        var Tariff = _Tariff.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
        if (Tariff == null)
        {
            result.AddError(ErrorCode.RecordAlreadyExists, "Record doesn't exist.");
            return result;
        }
        Tariff.UpdateRecordStatus(request.RecordStatus);
        _Tariff.Update(Tariff);

        result.Message = "Operation success";
        return result;
    }
}
