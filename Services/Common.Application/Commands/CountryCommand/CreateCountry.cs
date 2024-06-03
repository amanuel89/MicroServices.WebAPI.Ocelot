using AutoMapper;
using Common.Application.Models.Common;

namespace Common.Application.Commands.CountryCommand;
public class CreateCountry : IRequest<OperationResult<bool>>
{
    public CountryCreateRequest CountryRequest { get; set; }
}
public class CreateCountryHandler : IRequestHandler<CreateCountry, OperationResult<bool>>
{
    private readonly IRepositoryBase<Country> _countryRepository;
    public CreateCountryHandler(IRepositoryBase<Country> _countryRepository)
    {
        this._countryRepository = _countryRepository;
    }

    public async Task<OperationResult<bool>> Handle(CreateCountry request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();

        try
        {
            if (request.CountryRequest == null)
            {
                result.AddError(ErrorCode.ValidationError, "Request body cannot be empty");
                return result;
            }
            var data = request.CountryRequest;       
            var country = Country.Create(data.Name, data.PoliticalName, data.Continent, data.TelephoneCode, data.TimeZone, data.Nationality, data.CountryCode, data.Remark);
            await _countryRepository.AddAsync(country);
            result.Payload = true;
            result.Message = "Operation success";
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCode.UnknownError, $"error occurred Country not registered.");
        }

        return result;
    }
}
