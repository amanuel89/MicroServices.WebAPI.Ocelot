using AutoMapper;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using CommonService.Domain.Models;
using CommonService.Infrastructure.HttpService.Models;
using ErrorCode = Common.Application.Models.Common.ErrorCode;
using Google.Type;
using Common.Application.Models.Common;

namespace CommonService.Application.Queries
{
    public class GetCountry : IRequest<OperationResult<CountrySingleResponse>>
    {
        public long Id { get; set; }
    }
    public class GetCountryHandler : IRequestHandler<GetCountry, OperationResult<CountrySingleResponse>>
    {
        private readonly IRepositoryBase<Country> _countryRepository;
        private readonly IMapper _mapper;
        public GetCountryHandler(IRepositoryBase<Country> _countryRepository, IMapper mapper)
        {
            this._countryRepository = _countryRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<CountrySingleResponse>> Handle(GetCountry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CountrySingleResponse>();
            var country = await _countryRepository.Where(x => x.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
            if (country == null)
            {
                result.AddError(ErrorCode.NotFound, "Bank doesn't exist.");
                return result;
            }
            var response = _mapper.Map<CountrySingleResponse>(country);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
