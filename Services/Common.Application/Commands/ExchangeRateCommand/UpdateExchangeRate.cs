using AutoMapper;
using Common.Application.Models.Common;
using CommonService.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommonService.Application.Commands
{
    public class UpdateExchangeRate : IRequest<OperationResult<bool>>
    {
        public ExchangeRateUpdateRequest ExchangeRateUpdateRequest { get; set; }
    }

    public class UpdateExchangeRateHandler : IRequestHandler<UpdateExchangeRate, OperationResult<bool>>
    {
        private readonly IRepositoryBase<ExchangeRate> _exchangeRateRepository;

        public UpdateExchangeRateHandler(IRepositoryBase<ExchangeRate> exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository ?? throw new ArgumentNullException(nameof(exchangeRateRepository));
        }

        public async Task<OperationResult<bool>> Handle(UpdateExchangeRate request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            if (request.ExchangeRateUpdateRequest == null)
            {
                result.AddError(ErrorCode.NotFound, "Request body cannot be empty.");
                return result;
            }
            var exchangeRate = await _exchangeRateRepository.FirstOrDefaultAsync(x => x.Id == request.ExchangeRateUpdateRequest.Id && x.IsActive == true);
            if (exchangeRate == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }
            var data = request.ExchangeRateUpdateRequest;
            exchangeRate.Update(data.CurrencyId, data.Date, data.Buying, data.Selling);
            await _exchangeRateRepository.UpdateAsync(exchangeRate);
            result.Message = "Operation success";
            return result;
        }
    }
}
