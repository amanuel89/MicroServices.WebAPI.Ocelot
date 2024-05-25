using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetRideSettingsList : IRequest<OperationResult<PagedResponse<IEnumerable<SettingsResponseDto>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetRideSettingsListHandler : IRequestHandler<GetRideSettingsList, OperationResult<PagedResponse<IEnumerable<SettingsResponseDto>>>>
    {
        private readonly IRepositoryBase<RideSettings> _RideSettingsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetRideSettingsListHandler(IRepositoryBase<RideSettings> RideSettingsRepository, IMapper mapper)
        {
            _RideSettingsRepository = RideSettingsRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<SettingsResponseDto>>>> Handle(GetRideSettingsList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<SettingsResponseDto>>>();

            var query = _RideSettingsRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<RideSettings> data;
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

            var response = _mapper.Map<List<SettingsResponseDto>>(data);
            result.Payload = new PagedResponse<IEnumerable<SettingsResponseDto>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
