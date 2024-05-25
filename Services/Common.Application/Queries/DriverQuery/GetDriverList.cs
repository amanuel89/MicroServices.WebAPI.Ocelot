using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetDriverList : IRequest<OperationResult<PagedResponse<IEnumerable<DriverResponseDTO>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetDriverListHandler : IRequestHandler<GetDriverList, OperationResult<PagedResponse<IEnumerable<DriverResponseDTO>>>>
    {
        private readonly IRepositoryBase<Driver> _DriverRepository;
        private readonly IMapper _mapper;
        public GetDriverListHandler(IRepositoryBase<Driver> DriverRepository, IMapper mapper)
        {
            _DriverRepository = DriverRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<DriverResponseDTO>>>> Handle(GetDriverList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<DriverResponseDTO>>>();

            var query = _DriverRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<Driver> data;
            if (request.PageSize == 0 || request.PageNumber == 0)
            {
                data = await query.Include(x => x.Vehicle).ThenInclude(x => x.VehicleType).AsNoTracking().ToListAsync();
            }
            else
            {
                data = await query.Include(x => x.Vehicle).ThenInclude(x => x.VehicleType).Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
            }

            if (!data.Any())
            {
                result.AddError(ErrorCode.NotFound, "No record found");
                result.IsError = true;
            }

            var response = _mapper.Map<List<DriverResponseDTO>>(data);
            result.Payload = new PagedResponse<IEnumerable<DriverResponseDTO>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
