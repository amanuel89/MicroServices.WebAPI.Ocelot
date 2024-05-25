using AutoMapper;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using RideBackend.Domain.Models;
using RideBackend.Infrastructure.HttpService.Models;
using ErrorCode = RideBackend.Application.Models.ErrorCode;
using Google.Type;

namespace RideBackend.Application.Queries
{
    public class GetPassenger : IRequest<OperationResult<PassengerResponseDTO>>
    {
        public long Id { get; set; }
    }
    public class GetPassengerHandler : IRequestHandler<GetPassenger, OperationResult<PassengerResponseDTO>>
    {
        private readonly IRepositoryBase<Passenger> _PassengerRepository;
        private readonly IMapper _mapper;
        public GetPassengerHandler(IRepositoryBase<Passenger> PassengerRepository, IMapper mapper)
        {
            _PassengerRepository = PassengerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PassengerResponseDTO>> Handle(GetPassenger request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PassengerResponseDTO>();

            var passenger = _PassengerRepository.Where(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted).AsNoTracking().FirstOrDefault();

            if (passenger == null)
            {
                result.AddError(ErrorCode.NotFound, "Passenger doesn't exist.");
                return result;
            }
            var response = _mapper.Map<PassengerResponseDTO>(passenger);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
