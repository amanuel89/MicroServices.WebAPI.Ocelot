using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Common.Application.Commands.ExchangeRateCommand
{
    public class UpdateExchangeRateStatus : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class UpdateExchangeRateStatusHandler : IRequestHandler<UpdateExchangeRateStatus, OperationResult<bool>>
    {
        private readonly IRepositoryBase<ExchangeRate> _exchangeRateRepository;

        public UpdateExchangeRateStatusHandler(IRepositoryBase<ExchangeRate> exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateExchangeRateStatus request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var exchangeRate = await _exchangeRateRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (exchangeRate == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            exchangeRate.UpdateRecordStatus(!exchangeRate.IsActive);
            var updateResult = await _exchangeRateRepository.UpdateAsync(exchangeRate);

            if (updateResult)
            {
                result.Payload = true;
                result.Message = exchangeRate.IsActive
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
