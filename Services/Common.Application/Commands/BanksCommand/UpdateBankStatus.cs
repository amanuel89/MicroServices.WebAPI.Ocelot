//using Common.Application.Models.Common;

//namespace CommonService.Application.Commands;

//public class UpdateBanksStatus : IRequest<OperationResult<Bank>>
//{
//    public long Id { get; set; }
//    public RecordStatus RecordStatus { get; set; }
//}
//public class UpdateBanksStatusHandler : IRequestHandler<UpdateBanksStatus, OperationResult<Bank>>
//{
//    private readonly IRepositoryBase<Bank> _Banks;
//    public UpdateBanksStatusHandler(IRepositoryBase<Bank> _Banks) => this._Banks = _Banks;
//    public async Task<OperationResult<Bank>> Handle(UpdateBanksStatus request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<Bank>();
//        var Banks = _Banks.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);
//        if (Banks == null)
//        {
//            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
//            return result;
//        }
//        Banks.UpdateRecordStatus(request.RecordStatus);
//        _Banks.Update(Banks);

//        result.Payload = Banks;
//        result.Message = "Operation success";
//        return result;
//    }
//}
