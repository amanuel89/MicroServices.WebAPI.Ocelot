using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Common.Application.Commands.CountryCommand
{
    public class DeleteCountry : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class DeleteCountryHandler : IRequestHandler<DeleteCountry, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Country> _countryRepository;

        public DeleteCountryHandler(IRepositoryBase<Country> countryRepository) =>
            _countryRepository = countryRepository;

        public async Task<OperationResult<bool>> Handle(DeleteCountry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var country = await _countryRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (country == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            var deleteResult = await _countryRepository.RemoveAsync(country, cancellationToken);
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
