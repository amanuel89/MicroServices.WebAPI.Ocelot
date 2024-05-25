namespace RideBackend.Application.Commands;

public class DeleteTariff : IRequest<OperationResult<Tariff>>
{
    public long Id { get; set; }
}
internal class DeleteTariffHandler : IRequestHandler<DeleteTariff, OperationResult<Tariff>>
{

    private readonly IRepositoryBase<Tariff> _Tariff;
    public DeleteTariffHandler(IRepositoryBase<Tariff> _Tariff) => this._Tariff = _Tariff;
    public async Task<OperationResult<Tariff>> Handle(DeleteTariff request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Tariff>();

        var Tariff = _Tariff.FirstOrDefault(x => x.Id == request.Id);
        if (Tariff == null || Tariff.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }
        if (Tariff.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
        Tariff.Delete();
        _Tariff.Update(Tariff);
        result.Message = "Operation success";
        return result;
    }
}