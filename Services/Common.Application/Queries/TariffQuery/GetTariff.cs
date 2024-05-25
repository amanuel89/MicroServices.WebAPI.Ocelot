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
    public class GetTariff : IRequest<OperationResult<TariffResponseDto>>
    {
        public long Id { get; set; }
    }
    public class GetTariffHandler : IRequestHandler<GetTariff, OperationResult<TariffResponseDto>>
    {
        private readonly IRepositoryBase<Tariff> _tariff;
        private readonly IMapper _mapper;
        public GetTariffHandler(IRepositoryBase<Tariff> tariff, IMapper mapper)
        {
            _tariff = tariff;
            _mapper = mapper;
        }

        public async Task<OperationResult<TariffResponseDto>> Handle(GetTariff request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<TariffResponseDto>();

            var Tariff = _tariff.Where(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted).AsNoTracking().FirstOrDefault();

            if (Tariff == null)
            {
                result.AddError(ErrorCode.NotFound, "Tariff doesn't exist.");
                return result;
            }
            var response = _mapper.Map<TariffResponseDto>(Tariff);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
