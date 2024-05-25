using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetDriverVehicleHistory : IRequest<OperationResult<PagedResponse<IEnumerable<DriverVehicleResponse>>>>
    {
        public long DriverId { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetDriverVehicleHistoryHandler : IRequestHandler<GetDriverVehicleHistory, OperationResult<PagedResponse<IEnumerable<DriverVehicleResponse>>>>
    {
        private readonly IRepositoryBase<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;
        public GetDriverVehicleHistoryHandler(IRepositoryBase<Vehicle> vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<DriverVehicleResponse>>>> Handle(GetDriverVehicleHistory request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<DriverVehicleResponse>>>();

            var query = _vehicleRepository.Where(x => x.DriverId==request.DriverId);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<Vehicle> data;
            if (request.PageSize == 0 || request.PageNumber == 0)
            {
                data = await query.Include(x => x.VehicleType).AsNoTracking().ToListAsync();
            }
            else
            {
                data = await query.Include(x => x.VehicleType).Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
            }

            if (!data.Any())
            {
                result.AddError(ErrorCode.NotFound, "No record found");
                result.IsError = true;
            }

            var response = _mapper.Map<List<DriverVehicleResponse>>(data);
            result.Payload = new PagedResponse<IEnumerable<DriverVehicleResponse>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
