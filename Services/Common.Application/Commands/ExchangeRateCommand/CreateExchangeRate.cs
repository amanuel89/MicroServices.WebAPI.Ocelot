using AutoMapper;
using Common.Application.Models.Common;

namespace Common.Application.Commands.ExchangeRateCommand
{
    public class ExchangeRateCreateCommand : IRequest<OperationResult<bool>>
    {
        public ExchangeRateCreateRequest ExchangeRateRequest { get; set; }
    }

    public class CreateExchangeRateHandler : IRequestHandler<ExchangeRateCreateCommand, OperationResult<bool>>
    {
        private readonly IRepositoryBase<ExchangeRate> _exchangeRateRepository;

        public CreateExchangeRateHandler(IRepositoryBase<ExchangeRate> exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<OperationResult<bool>> Handle(ExchangeRateCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                if (request.ExchangeRateRequest == null)
                {
                    result.AddError(ErrorCode.ValidationError, "Request body cannot be empty");
                    return result;
                }

                var data = request.ExchangeRateRequest;
                var exchangeRate = ExchangeRate.Create(data.CurrencyId, data.Date, data.Buying, data.Selling);
                await _exchangeRateRepository.AddAsync(exchangeRate);
                result.Payload = true;
                result.Message = "Operation success";
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.UnknownError, "An error occurred. Exchange rate not registered.");
            }

            return result;
        }
    }
}
