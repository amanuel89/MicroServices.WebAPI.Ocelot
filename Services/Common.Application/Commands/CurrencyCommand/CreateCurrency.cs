using AutoMapper;
using Common.Application.Models.Common;

namespace Common.Application.Commands.CurrencyCommand
{
    public class CurrencyCreateCommand : IRequest<OperationResult<bool>>
    {
        public CurrencyCreateRequest CurrencyRequest { get; set; }
    }

    public class CreateCurrencyHandler : IRequestHandler<CurrencyCreateCommand, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Currency> _currencyRepository;

        public CreateCurrencyHandler(IRepositoryBase<Currency> currencyRepository)
        {
            this._currencyRepository = currencyRepository;
        }

        public async Task<OperationResult<bool>> Handle(CurrencyCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                if (request.CurrencyRequest == null)
                {
                    result.AddError(ErrorCode.ValidationError, "Request body cannot be empty");
                    return result;
                }

                var data = request.CurrencyRequest;
                var currency = Currency.Create(data.CountryId, data.Description, data.Sign, data.Abbreviation, data.IsDefault);
                await _currencyRepository.AddAsync(currency);
                result.Payload = true;
                result.Message = "Operation success";
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.UnknownError, "An error occurred. Currency not registered.");
            }

            return result;
        }
    }
}
