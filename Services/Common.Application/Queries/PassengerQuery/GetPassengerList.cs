using RideBackend.Domain.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace RideBackend.Application.Queries
{
    public class GetPassengerList : IRequest<OperationResult<PagedResponse<IEnumerable<PassengerResponseDTO>>>>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public RecordStatus? RecordStatus { get; set; }
    }

    public class GetPassengerListHandler : IRequestHandler<GetPassengerList, OperationResult<PagedResponse<IEnumerable<PassengerResponseDTO>>>>
    {
        private readonly IRepositoryBase<Passenger> _passengerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetPassengerListHandler(IRepositoryBase<Passenger> passengerRepository, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PagedResponse<IEnumerable<PassengerResponseDTO>>>> Handle(GetPassengerList request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResponse<IEnumerable<PassengerResponseDTO>>>();

            var query = _passengerRepository.Where(x => x.RecordStatus != RecordStatus.Deleted);

            if (request.RecordStatus.HasValue)
            {
                query = query.Where(x => x.RecordStatus == request.RecordStatus.Value);
            }

            var count = await query.CountAsync();
            query = query.OrderByDescending(x => x.LastUpdateDate);

            IEnumerable<Passenger> data;
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

            var response = _mapper.Map<List<PassengerResponseDTO>>(data);
            result.Payload = new PagedResponse<IEnumerable<PassengerResponseDTO>>(response, request.PageNumber, request.PageSize, count);
            result.Message = "Operation Success";
            return result;
        }

    }
}
