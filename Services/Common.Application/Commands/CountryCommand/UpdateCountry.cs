using AutoMapper;
using Common.Application.Models.Common;
using CommonService.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommonService.Application.Commands
{
    public class UpdateCountry : IRequest<OperationResult<bool>>
    {
        public CountryUpdateRequest CountryUpdateRequest { get; set; }
    }

    public class UpdateCountryHandler : IRequestHandler<UpdateCountry, OperationResult<bool>>
    {
        private readonly IRepositoryBase<Country> _countryRepository;

        public UpdateCountryHandler(IRepositoryBase<Country> countryRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public async Task<OperationResult<bool>> Handle(UpdateCountry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();
            if (request.CountryUpdateRequest == null)
            {
                result.AddError(ErrorCode.NotFound, "Request body cannot be empty.");
                return result;
            }
            var country = await _countryRepository.FirstOrDefaultAsync(x => x.Id == request.CountryUpdateRequest.Id && x.IsActive == true);
            if (country == null)
            {
                result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
                return result;
            }
            var data = request.CountryUpdateRequest;
            country.Update(data.Name, data.PoliticalName, data.Continent, data.TelephoneCode, data.TimeZone, data.Nationality, data.CountryCode,data.Remark);
            await _countryRepository.UpdateAsync(country);
            result.Message = "Operation success";
            return result;
        }
    }
}
