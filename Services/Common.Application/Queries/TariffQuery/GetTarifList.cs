using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetTarifList : IRequest<OperationResult<PagedResponse<IEnumerable<TariffResponseDto>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetTarifListHandler : IRequestHandler<GetTarifList, OperationResult<PagedResponse<IEnumerable<TariffResponseDto>>>>
    {
        private readonly IRepositoryBase<Tariff> _TariffRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetTarifListHandler(IRepositoryBase<Tariff> TariffRepository, IMapper mapper)
        {
            _TariffRepository = TariffRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<TariffResponseDto>>>> Handle(GetTarifList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<TariffResponseDto>>>();

            var query = _TariffRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<Tariff> data;
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

            var response = _mapper.Map<List<TariffResponseDto>>(data);
            result.Payload = new PagedResponse<IEnumerable<TariffResponseDto>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
