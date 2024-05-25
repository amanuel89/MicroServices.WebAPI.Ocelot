using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetVehicleTypesList : IRequest<OperationResult<PagedResponse<IEnumerable<VehicleTypesDTo>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetVehicleTypesListHandler : IRequestHandler<GetVehicleTypesList, OperationResult<PagedResponse<IEnumerable<VehicleTypesDTo>>>>
    {
        private readonly IRepositoryBase<VehicleTypes> _VehicleTypesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetVehicleTypesListHandler(IRepositoryBase<VehicleTypes> VehicleTypesRepository, IMapper mapper)
        {
            _VehicleTypesRepository = VehicleTypesRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<VehicleTypesDTo>>>> Handle(GetVehicleTypesList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<VehicleTypesDTo>>>();

            var query = _VehicleTypesRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<VehicleTypes> data;
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

            var response = _mapper.Map<List<VehicleTypesDTo>>(data);
            result.Payload = new PagedResponse<IEnumerable<VehicleTypesDTo>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
