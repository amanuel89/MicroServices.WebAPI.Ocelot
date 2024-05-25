using AutoMapper;
using RideBackend.Domain.Models;
using RideBackend.Infrastructure.HttpService.Models;

namespace RideBackend.Application.Queries
{
    public class GetVehicleTypes : IRequest<OperationResult<VehicleTypesDTo>>
    {
        public long Id { get; set; }
    }
    public class GetVehicleTypesHandler : IRequestHandler<GetVehicleTypes, OperationResult<VehicleTypesDTo>>
    {
        private readonly IRepositoryBase<VehicleTypes> _VehicleTypesRepository;
        private readonly IMapper _mapper;
        public GetVehicleTypesHandler(IRepositoryBase<VehicleTypes> VehicleTypesRepository,IMapper mapper)
        {
            _VehicleTypesRepository = VehicleTypesRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<VehicleTypesDTo>> Handle(GetVehicleTypes request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<VehicleTypesDTo>();

            var VehicleTypes =  _VehicleTypesRepository.Where(x=>x.Id==request.Id && x.RecordStatus != RecordStatus.Deleted).AsNoTracking().FirstOrDefault();

            if (VehicleTypes == null)
            {
                result.AddError(ErrorCode.NotFound, "Vehicle Type doesn't exist.");
                return result;
            }
            var response = _mapper.Map<VehicleTypesDTo>(VehicleTypes);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
