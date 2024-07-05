using Common.Application.Models.Common;

namespace Common.Application.Commands.ExchangeRateCommand
{
    public class DeleteExchangeRate : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteExchangeRateHandler : IRequestHandler<DeleteExchangeRate, OperationResult<bool>>
    {
        private readonly IRepositoryBase<ExchangeRate> _exchangeRateRepository;

        public DeleteExchangeRateHandler(IRepositoryBase<ExchangeRate> exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteExchangeRate request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var exchangeRate = await _exchangeRateRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (exchangeRate == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            var deleteResult = await _exchangeRateRepository.RemoveAsync(exchangeRate, cancellationToken);
            if (deleteResult)
            {
                result.Payload = true;
                result.Message = "The Record is Deleted Successfully";
            }
            else
            {
                result.AddError(ErrorCode.UnknownError, "Error occurred while deleting the record.");
            }

            return result;
        }
    }
}
