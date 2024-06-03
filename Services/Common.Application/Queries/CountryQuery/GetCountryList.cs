using CommonService.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Common.Application.Models.Common;

namespace CommonService.Application.Queries
{
    public class GetCountryList : IRequest<OperationResult<PagedResponse<IEnumerable<CountryListResponse>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool Active { get; set; }=true;
    }

    public class GetCountryListHandler : IRequestHandler<GetCountryList, OperationResult<PagedResponse<IEnumerable<CountryListResponse>>>>
    {
        private readonly IRepositoryBase<Country> _countryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetCountryListHandler(IRepositoryBase<Country> _countryRepository, IMapper _mapper)
        {
            this._countryRepository = _countryRepository;
            this._mapper = _mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<CountryListResponse>>>> Handle(GetCountryList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<CountryListResponse>>>();

            var query = _countryRepository.Where(x => x.IsActive == request.Active);
            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateOn);

            IEnumerable<Country> data;
            if (request.PageSize == 0 || request.PageNumber == 0)
            {
                data = await query.AsNoTracking().ToListAsync();
            }
            else
            {
                data = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
            }

            if (!data.Any())
            {
                result.AddError(ErrorCode.NotFound, "No record found");
                result.IsError = true;
            }

            var response = _mapper.Map<List<CountryListResponse>>(data);
            result.Payload = new PagedResponse<IEnumerable<CountryListResponse>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
