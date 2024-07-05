using AutoMapper;
using Common.Application.Models.Common;
using CommonService.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommonService.Application.Commands
{
    public class UpdateCurrency : IRequest<OperationResult<bool>>
    {
        public CurrencyUpdateRequest CurrencyUpdateRequest { get; set; }
    }

    public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrency, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Currency> _currencyRepository;

        public UpdateCurrencyHandler(IRepositoryBase<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public async Task<OperationResult<bool>> Handle(UpdateCurrency request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            if (request.CurrencyUpdateRequest == null)
            {
                result.AddError(ErrorCode.NotFound, "Request body cannot be empty.");
                return result;
            }
            var currency = await _currencyRepository.FirstOrDefaultAsync(x => x.Id == request.CurrencyUpdateRequest.Id && x.IsActive == true);
            if (currency == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }
            var data = request.CurrencyUpdateRequest;
            currency.Update(data.CountryId, data.Description, data.Sign, data.Abbreviation, data.IsDefault);
            await _currencyRepository.UpdateAsync(currency);
            result.Message = "Operation success";
            return result;
        }
    }
}
