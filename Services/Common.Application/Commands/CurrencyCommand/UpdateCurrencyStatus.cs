using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Common.Application.Commands.CurrencyCommand
{
    public class UpdateSystemLookUpStatus : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class UpdateCurrencyStatusHandler : IRequestHandler<UpdateSystemLookUpStatus, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Currency> _currencyRepository;

        public UpdateCurrencyStatusHandler(IRepositoryBase<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateSystemLookUpStatus request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var currency = await _currencyRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (currency == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            currency.UpdateRecordStatus(!currency.IsActive);
            var updateResult = await _currencyRepository.UpdateAsync(currency);

            if (updateResult)
            {
                result.Payload = true;
                result.Message = currency.IsActive
                    ? "The Record is Activated Successfully"
                    : "The Record is Deactivated Successfully";
            }
            else
            {
                result.AddError(ErrorCode.UnknownError, "Error occurred while updating the record.");
            }

            result.Payload = updateResult;
            return result;
        }
    }
}
