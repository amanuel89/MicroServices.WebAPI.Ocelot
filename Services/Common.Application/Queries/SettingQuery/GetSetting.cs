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
    public class GetRideSettings : IRequest<OperationResult<SettingsResponseDto>>
    {
        public long Id { get; set; }
    }
    public class GetRideSettingsHandler : IRequestHandler<GetRideSettings, OperationResult<SettingsResponseDto>>
    {
        private readonly IRepositoryBase<RideSettings> _RideSettings;
        private readonly IMapper _mapper;
        public GetRideSettingsHandler(IRepositoryBase<RideSettings> RideSettings, IMapper mapper)
        {
            _RideSettings = RideSettings;
            _mapper = mapper;
        }

        public async Task<OperationResult<SettingsResponseDto>> Handle(GetRideSettings request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<SettingsResponseDto>();

            var RideSettings = _RideSettings.Where(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted).AsNoTracking().FirstOrDefault();

            if (RideSettings == null)
            {
                result.AddError(ErrorCode.NotFound, "RideSettings doesn't exist.");
                return result;
            }
            var response = _mapper.Map<SettingsResponseDto>(RideSettings);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
