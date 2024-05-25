using AutoMapper;
using RideBackend.Domain.Models;
using RideBackend.Infrastructure.HttpService.Models;

namespace RideBackend.Application.Queries
{
    public class GetDriverByUserName : IRequest<OperationResult<DriverResponseDTO>>
    {
        public string Username { get; set; }
    }
    public class GetDriverByUserNameHandler : IRequestHandler<GetDriverByUserName, OperationResult<DriverResponseDTO>>
    {
        private readonly IRepositoryBase<Driver> _DriverRepository;
        private readonly IMapper _mapper;
        public GetDriverByUserNameHandler(IRepositoryBase<Driver> DriverRepository,IMapper mapper)
        {
            _DriverRepository = DriverRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<DriverResponseDTO>> Handle(GetDriverByUserName request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<DriverResponseDTO>();

            var Driver =  _DriverRepository.Where(x=>x.DriverUserName==request.Username && x.RecordStatus!=RecordStatus.Deleted).Include(x => x.Vehicle.Where(x=>x.RecordStatus==RecordStatus.Active)).ThenInclude(x=>x.VehicleType).AsNoTracking().FirstOrDefault();

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
