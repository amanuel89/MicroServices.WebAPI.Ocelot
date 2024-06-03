using Common.Application.Models.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Common.Application.Commands.CountryCommand
{
    public class UpdateCountryStatus : IRequest<OperationResult<bool>>
    {
        public long Id { get; set; }
    }

    public class UpdateCountryStatusHandler : IRequestHandler<UpdateCountryStatus, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Country> _countryRepository;

        public UpdateCountryStatusHandler(IRepositoryBase<Country> countryRepository) =>
            _countryRepository = countryRepository;

        public async Task<OperationResult<bool>> Handle(UpdateCountryStatus request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            var country = await _countryRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (country == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }

            country.UpdateRecordStatus(!country.IsActive);
            var updateResult = await _countryRepository.UpdateAsync(country);

            if (updateResult)
            {
                result.Payload = true;
                result.Message = country.IsActive
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
