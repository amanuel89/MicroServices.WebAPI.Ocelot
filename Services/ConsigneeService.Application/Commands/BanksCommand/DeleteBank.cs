using Common.Application.Models.Common;

namespace ConsigneeService.Application.Commands;

public class DeleteBanks : IRequest<OperationResult<Bank>>
{
    public long Id { get; set; }
}
internal class DeleteBanksHandler : IRequestHandler<DeleteBanks, OperationResult<Bank>>
{

    private readonly IRepositoryBase<Bank> _Banks;
    public DeleteBanksHandler(IRepositoryBase<Bank> _Banks) => this._Banks = _Banks;
    public async Task<OperationResult<Bank>> Handle(DeleteBanks request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Bank>();

        var Banks = _Banks.FirstOrDefault(x => x.Id == request.Id);

        if (Banks == null || Banks.RecordStatus == RecordStatus.Deleted)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        if (Banks.IsReadOnly)
        {
            result.AddError(ErrorCode.Forbidden, "Record cannot be deleted.Record Is Read Only");
            return result;
        }
   
        Banks.Delete();
        _Banks.Update(Banks);

        result.Payload = Banks;
        result.Message = "Operation success";
        return result;
    }
}