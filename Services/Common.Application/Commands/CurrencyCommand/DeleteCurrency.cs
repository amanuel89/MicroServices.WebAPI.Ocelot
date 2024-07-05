
using Common.Application.Models.Common;

namespace Common.Application.Commands.CurrencyCommand
{
    public class DeleteSystemLookUp : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteCurrencyHandler : IRequestHandler<DeleteSystemLookUp, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Currency> _currencyRepository;

        public DeleteCurrencyHandler(IRepositoryBase<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteSystemLookUp request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var currency = await _currencyRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (currency == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            var deleteResult = await _currencyRepository.RemoveAsync(currency, cancellationToken);
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
