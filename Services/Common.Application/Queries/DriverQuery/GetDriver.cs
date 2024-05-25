using AutoMapper;
using RideBackend.Domain.Models;
using RideBackend.Infrastructure.HttpService.Models;

namespace RideBackend.Application.Queries
{
    public class GetDriver : IRequest<OperationResult<DriverResponseDTO>>
    {
        public long Id { get; set; }
    }
    public class GetDriverHandler : IRequestHandler<GetDriver, OperationResult<DriverResponseDTO>>
    {
        private readonly IRepositoryBase<Driver> _DriverRepository;
        private readonly IMapper _mapper;
        public GetDriverHandler(IRepositoryBase<Driver> DriverRepository,IMapper mapper)
        {
            _DriverRepository = DriverRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<DriverResponseDTO>> Handle(GetDriver request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<DriverResponseDTO>();

            var Driver =  _DriverRepository.Where(x=>x.Id==request.Id && x.RecordStatus!=RecordStatus.Deleted).Include(x => x.Vehicle.Where(x=>x.RecordStatus==RecordStatus.Active)).ThenInclude(x=>x.VehicleType).AsNoTracking().FirstOrDefault();

            if (Driver == null)
            {
                result.AddError(ErrorCode.NotFound, "Driver doesn't exist.");
                return result;
            }
            var response = _mapper.Map<DriverResponseDTO>(Driver);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
