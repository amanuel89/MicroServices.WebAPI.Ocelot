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
    public class GetBank : IRequest<OperationResult<BankResponseDTO>>
    {
        public long Id { get; set; }
    }
    public class GetBankHandler : IRequestHandler<GetBank, OperationResult<BankResponseDTO>>
    {
        private readonly IRepositoryBase<Bank> _BankRepository;
        private readonly IMapper _mapper;
        public GetBankHandler(IRepositoryBase<Bank> BankRepository, IMapper mapper)
        {
            _BankRepository = BankRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<BankResponseDTO>> Handle(GetBank request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BankResponseDTO>();

            var Bank = _BankRepository.Where(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted).AsNoTracking().FirstOrDefault();

            if (Bank == null)
            {
                result.AddError(ErrorCode.NotFound, "Bank doesn't exist.");
                return result;
            }
            var response = _mapper.Map<BankResponseDTO>(Bank);
            result.Payload = response;
            result.Message = "Operation success";
            return result;
        }
    }
}
